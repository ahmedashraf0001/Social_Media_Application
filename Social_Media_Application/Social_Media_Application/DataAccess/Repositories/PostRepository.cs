using Microsoft.EntityFrameworkCore;
using Social_Media_Application.Common.DTOs;
using Social_Media_Application.Common.Entities;
using Social_Media_Application.Common.Utils;
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

        public async Task<List<PostDTO>> GenerateFeedAsync(string currentUserId, string userId, int pageNumber, int pageSize = 12)
        {
            var followeduserIds = await _context.userFollows
                .Where(e => e.FollowedId.Equals(userId))
                .Select(e => e.FollowerId).ToListAsync();

            if(followeduserIds.Count == 0)
            {
                return new List<PostDTO>();
            }

            var posts = await _context.posts
                .Where(e => followeduserIds.Contains(e.UserId))
                .Include(e => e.User)
                .OrderByDescending(e => e.LikesCount)
                .ThenByDescending(e => e.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            List<PostDTO> postsDTO = new List<PostDTO>();   
            foreach (var post in posts)
            {
                bool isLikedByCurrentUser = await _context.postLikes
                                                 .AnyAsync(l => l.PostId == post.Id && l.UserId == currentUserId);
                PostDTO model = new PostDTO()
                {
                    Id = post.Id,
                    Content = post.Content,
                    MediaUrl = post.MediaUrl,
                    MediaType = post.MediaType,
                    CreatedAt = post.CreatedAt,
                    UserId = post.UserId,
                    AuthorUsername = post.User.FirstName + " " + post.User.LastName,
                    LikeCount = post.LikesCount,
                    CommentCount = post.CommentsCount,
                    IsLikedByCurrentUser = isLikedByCurrentUser
                };
                postsDTO.Add(model);
            }
            return postsDTO;
        }
    }
}
