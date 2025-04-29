using Microsoft.EntityFrameworkCore;
using Social_Media_Application.Common.DTOs;
using Social_Media_Application.Common.Entities;
using Social_Media_Application.Common.Utils.Queries;
using Social_Media_Application.DataAccess.Data;
using Social_Media_Application.DataAccess.Interfaces;

namespace Social_Media_Application.DataAccess.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly SocialDBContext _context;
        public PostRepository(SocialDBContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Post>> GetPostsByUserIdAsync(string userId, PostQueryOptions options)
        {
            IQueryable<Post> query = _set.Where(u => u.UserId == userId);

            if (options.IncludeLikedUsers)
            {
                query = query.Include(u => u.Likes);
            }

            if (options.IncludeComments)
            {
                query = query.Include(u => u.Comments);
            }

            if (options.IncludeAuthorDetails)
            {
                query = query.Include(u => u.User);
            }

            if (options.PageNumber.HasValue && options.PageSize.HasValue)
            {
                int skip = (options.PageNumber.Value - 1) * options.PageSize.Value;
                query = query.Skip(skip).Take(options.PageSize.Value);
            }

            return await query.ToListAsync();

        }
        public async Task<Post> GetPostAsync(int Id, PostQueryOptions options)
        {
            IQueryable<Post> query = _set.Where(p => p.Id == Id);

            if (options.IncludeLikedUsers)
            {
                query = query.Include(u => u.Likes).ThenInclude(u => u.User);
            }

            if (options.IncludeComments)
            {
                query = query.Include(u => u.Comments);
            }

            if (options.IncludeAuthorDetails)
            {
                query = query.Include(u => u.User);
            }

            return await query.FirstOrDefaultAsync(); 
        }
        public override async Task<List<Post>> GetAllAsync(object options)
        {
            IQueryable<Post> query = _set;

            if (options is PostQueryOptions postOptions)
            {
                if (postOptions.IncludeLikedUsers)
                {
                    query = query.Include(p => p.Likes);
                }

                if (postOptions.IncludeComments)
                {
                    query = query.Include(p => p.Comments);
                }

                if (postOptions.IncludeAuthorDetails)
                {
                    query = query.Include(p => p.User);
                }
            }

            return await query.ToListAsync();
        }

        public async Task<List<PostDTO>> GenerateFeedAsync(string currentUserId, PostQueryOptions options)
        {
            var followedUserIds = await _context.userFollows
                .Where(e => e.FollowerId.Equals(currentUserId))
                .Select(e => e.FollowedId)
                .ToListAsync();

            if (followedUserIds.Count == 0)
            {
                return new List<PostDTO>();
            }

            IQueryable<Post> query = _context.posts
                .Where(e => followedUserIds.Contains(e.UserId))
                .OrderByDescending(e => e.LikesCount)
                .ThenByDescending(e => e.CreatedAt);

            if (options.IncludeLikedUsers)
                query = query.Include(e => e.Likes);

            if (options.IncludeComments)
                query = query.Include(e => e.Comments);

            if (options.IncludeAuthorDetails)
                query = query.Include(e => e.User);

            if (options.PageNumber.HasValue && options.PageSize.HasValue)
            {
                int skip = (options.PageNumber.Value - 1) * options.PageSize.Value;
                query = query.Skip(skip).Take(options.PageSize.Value);
            }

            var posts = await query.ToListAsync();

            var postsDTO = new List<PostDTO>();

            foreach (var post in posts)
            {
                bool isLikedByCurrentUser = await _context.postLikes
                    .AnyAsync(l => l.PostId == post.Id && l.UserId == currentUserId);

                var model = new PostDTO
                {
                    Id = post.Id,
                    Content = post.Content,
                    MediaUrl = post.MediaUrl,
                    MediaType = post.MediaType,
                    CreatedAt = post.CreatedAt,
                    UserId = post.UserId,
                    AuthorUsername = post.User?.FirstName + " " + post.User?.LastName,
                    LikeCount = post.LikesCount,
                    CommentCount = post.CommentsCount,
                    IsLikedByCurrentUser = isLikedByCurrentUser
                };

                postsDTO.Add(model);
            }

            return postsDTO;
        }
        public async Task<List<Post>> SearchPostsAsync(PostSearchQuery searchQuery, PostQueryOptions queryOptions)
        {
            var query = _context.posts.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery.Keyword))
            {
                query = query.Where(p => p.Content.Contains(searchQuery.Keyword));
            }

            if (!string.IsNullOrEmpty(searchQuery.UserId))
            {
                query = query.Where(p => p.UserId == searchQuery.UserId);
            }

            if (searchQuery.StartDate.HasValue)
            {
                query = query.Where(p => p.CreatedAt >= searchQuery.StartDate.Value);
            }

            if (searchQuery.EndDate.HasValue)
            {
                query = query.Where(p => p.CreatedAt <= searchQuery.EndDate.Value);
            }

            if (searchQuery.MediaType.HasValue)
            {
                query = query.Where(p => p.MediaType == searchQuery.MediaType.Value);
            }

            if (queryOptions.IncludeLikedUsers)
            {
                query = query.Include(p => p.Likes); 
            }

            if (queryOptions.IncludeComments)
            {
                query = query.Include(p => p.Comments);  
            }

            if (queryOptions.IncludeAuthorDetails)
            {
                query = query.Include(p => p.User); 
            }

            if (queryOptions.PageNumber.HasValue && queryOptions.PageSize.HasValue)
            {
                query = query.Skip((queryOptions.PageNumber.Value - 1) * queryOptions.PageSize.Value)
                             .Take(queryOptions.PageSize.Value);
            }

            query = query.OrderByDescending(p => p.CreatedAt);

            var result = await query.ToListAsync();

            return result;
        }


    }
}
