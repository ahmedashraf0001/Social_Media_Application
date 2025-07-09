using Microsoft.EntityFrameworkCore;
using Social_Media_Application.Common.Entities;
using Social_Media_Application.Common.Utils.Queries;
using Social_Media_Application.DataAccess.Data;
using Social_Media_Application.DataAccess.Interfaces;
using System.Collections.Generic;

namespace Social_Media_Application.DataAccess.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly SocialDBContext _context;

        public UserRepository(SocialDBContext context) : base(context)
        {
            _context = context;
        }
        public async Task<User> GetUserAsync(string Id, UserQueryOptions options)
        {
            IQueryable<User> query = _set.Where(p => p.Id == Id);


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
        public async Task<User?> GetByUsernameAsync(string username, UserQueryOptions options)
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
        public async Task<List<User>> GetAllAsync(int pageNumber, int pageSize = 12)
        {
            var query = await _set
                .Select(u => new User
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    PhotoUrl = u.PhotoUrl,
                    FollowersCount = u.FollowersCount,
                    FollowingCount = u.FollowingCount,
                })
                .ToListAsync();
            var pagedResult = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return pagedResult;
        }
        public async Task DeleteUserAsync(string userId)
        {
            var affectedConversations = await _context.conversations
                .Where(c => c.CurrentUserId == userId || c.otherUserId == userId)
                .ToListAsync();

            foreach (var conversation in affectedConversations)
            {
                bool otherUserExists = conversation.CurrentUserId == userId
                    ? await _context.Users.AnyAsync(u => u.Id == conversation.otherUserId)
                    : await _context.Users.AnyAsync(u => u.Id == conversation.CurrentUserId);

                if (!otherUserExists)
                {
                    _context.conversations.Remove(conversation);
                }
            }
            await _context.SaveChangesAsync();
        }
        public async Task<List<User>> SearchUsersAsync(string searchTerm, int pageNumber, int pageSize = 12)
        {
            var query = new List<User>();
            if (string.IsNullOrEmpty(searchTerm))
            {
                return query;
            }
            query = await _set
                .Where(u => u.UserName.Contains(searchTerm) || (u.FirstName+" "+u.LastName).Contains(searchTerm)).Select(u => new User
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    PhotoUrl = u.PhotoUrl,
                    FollowersCount = u.FollowersCount,
                    FollowingCount = u.FollowingCount,
                })
                .ToListAsync();
            var pagedResult = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return pagedResult;
        }
    }
}