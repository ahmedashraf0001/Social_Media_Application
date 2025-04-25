using Microsoft.EntityFrameworkCore;
using Social_Media_Application.Common.Entities;
using Social_Media_Application.DataAccess.Data;
using Social_Media_Application.DataAccess.Interfaces;

namespace Social_Media_Application.DataAccess.Repositories
{
    public class UserFollowRepository : Repository<UserFollow>, IUserFollowRepository
    {
        private readonly SocialDBContext _context;
        public UserFollowRepository(SocialDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<UserFollow>> GetFollowers(string userId)
        {
            return await _context.userFollows
                .Where(uf => uf.FollowedId == userId)
                .Include(uf => uf.Follower)
                .ThenInclude(uf => uf.Posts)
                .ToListAsync();
        }

        public async Task<List<UserFollow>> GetFollowed(string userId)
        {
            return await _context.userFollows
                .Where(uf => uf.FollowerId == userId)
                .Include(uf => uf.Followed)
                .ThenInclude(uf => uf.Posts)
                .ToListAsync();
        }
    }
}
