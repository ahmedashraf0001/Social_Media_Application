using Microsoft.EntityFrameworkCore;
using Social_Media_Application.Common.Entities;
using Social_Media_Application.Common.Utils;
using Social_Media_Application.DataAccess.Data;
using Social_Media_Application.DataAccess.Interfaces;
using System.Collections.Generic;

namespace Social_Media_Application.DataAccess.Repositories
{
    public class UserRepository : Repository<User>
    {
        private readonly SocialDBContext _context;

        public UserRepository(SocialDBContext context) : base(context)
        {
            _context = context;
        }
        public async Task<User?> GetByUsernameAsync(object username, UserQueryOptions options)
        {
            IQueryable<User> query = _set.Where(u => u.UserName == username);

            if (options.WithPosts)
            {
                query = query.Include(u => u.Posts);
            }

            if (options.WithComments)
            {
                query = query.Include(u => u.Comments);
            }

            if (options.WithLikedPosts)
            {
                query = query.Include(u => u.LikedPosts);
            }

            if (options.WithFollowers)
            {
                query = query.Include(u => u.Followers);
            }

            if (options.WithFollowing)
            {
                query = query.Include(u => u.Following);
            }

            return await query.FirstOrDefaultAsync();
        }

        public override async Task<List<User>> GetAllAsync(UserQueryOptions options)
        {
            IQueryable<User> query = _set;

            if (options.WithPosts)
            {
                query = query.Include(u => u.Posts);
            }

            if (options.WithComments)
            {
                query = query.Include(u => u.Comments);
            }

            if (options.WithLikedPosts)
            {
                query = query.Include(u => u.LikedPosts);
            }

            if (options.WithFollowers)
            {
                query = query.Include(u => u.Followers);
            }

            if (options.WithFollowing)
            {
                query = query.Include(u => u.Following);
            }

            return await query.ToListAsync();
        }

    }
}