using Microsoft.EntityFrameworkCore;
using Social_Media_Application.Common.Entities;
using Social_Media_Application.Common.Utils;
using Social_Media_Application.DataAccess.Data;

namespace Social_Media_Application.DataAccess.Repositories
{
    public class PostRepository : Repository<Post>
    {
        private readonly SocialDBContext _context;
        public PostRepository(SocialDBContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Post>> GetPostsByUserIdAsync(string userId, UserQueryOptions options)
        {
            IQueryable<Post> query = _set.Where(u => u.UserId == userId);

            if (options.WithLikedPosts)
            {
                query = query.Include(u => u.Likes);
            }

            if (options.WithComments)
            {
                query = query.Include(u => u.Comments);
            }

            return await query.ToListAsync();

        }
        public override async Task<List<Post>> GetAllAsync(UserQueryOptions options)
        {
            IQueryable<Post> query = _set;

            if (options.WithLikedPosts)
            {
                query = query.Include(u => u.Likes);
            }

            if (options.WithComments)
            {
                query = query.Include(u => u.Comments);
            }

            return await query.ToListAsync();
        }
    }
}
