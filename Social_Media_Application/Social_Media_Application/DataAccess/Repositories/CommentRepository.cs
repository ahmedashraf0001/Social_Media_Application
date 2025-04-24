using Microsoft.EntityFrameworkCore;
using Social_Media_Application.Common.Entities;
using Social_Media_Application.Common.Utils;
using Social_Media_Application.DataAccess.Data;

namespace Social_Media_Application.DataAccess.Repositories
{
    public class CommentRepository : Repository<Comment>
    {
        private readonly SocialDBContext _context;

        public CommentRepository(SocialDBContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Comment>> GetByPostIdAsync(int postId, UserQueryOptions options)
        {
            IQueryable<Comment> query = _set.Where(u => u.PostId == postId);

            if (options.WithPosts)
            {
                query = query.Include(u => u.Post);
            }

            if (options.WithUsers)
            {
                query = query.Include(u => u.User);
            }

            return await query.ToListAsync();
        }
        public async Task<List<Comment>> GetByUserIdAsync(string userId, UserQueryOptions options)
        {
            IQueryable<Comment> query = _set.Where(u => u.UserId == userId);

            if (options.WithPosts)
            {
                query = query.Include(u => u.Post);
            }

            if (options.WithUsers)
            {
                query = query.Include(u => u.User);
            }

            return await query.ToListAsync();
        }
        public override async Task<List<Comment>> GetAllAsync(UserQueryOptions options)
        {
            IQueryable<Comment> query = _set;

            if (options.WithPosts)
            {
                query = query.Include(u => u.Post);
            }

            if (options.WithUsers)
            {
                query = query.Include(u => u.User);
            }

            return await query.ToListAsync();
        }
    }
}
