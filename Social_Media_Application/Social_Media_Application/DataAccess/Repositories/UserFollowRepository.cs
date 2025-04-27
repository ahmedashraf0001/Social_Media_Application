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

        public async Task<List<UserFollow>> GetFollowersAsync(string userId, int pageNumber, int pageSize = 12)
        {
            var followers = await _context.userFollows
                .Where(uf => uf.FollowedId == userId)
                .Include(uf => uf.Follower)
                .ThenInclude(uf => uf.Posts)
                .ToListAsync();
            var pagedResult = followers
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return pagedResult;
        }

        public async Task<List<UserFollow>> GetFollowedAsync(string userId, int pageNumber, int pageSize = 12)
        {
            var followedUsers = await _context.userFollows
                .Where(uf => uf.FollowerId == userId)
                .Include(uf => uf.Followed)
                .ThenInclude(uf => uf.Posts)
                .ToListAsync();
            var pagedResult = followedUsers
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return pagedResult;
        }
        public async Task ToggleFollowAsync(string followingId, string followedId)
        {
            var userFollow = await _set
                .FirstOrDefaultAsync(uf => uf.FollowerId == followingId && uf.FollowedId == followedId);

            var follower = await _context.Users.FindAsync(followingId);
            var followed = await _context.Users.FindAsync(followedId);

            if (follower == null || followed == null)
            {
                throw new Exception("One or both users not found");
            }

            if (userFollow != null)
            {
                _set.Remove(userFollow);
                if (follower.FollowingCount > 0)
                    follower.FollowingCount--;
                if (followed.FollowersCount > 0)
                    followed.FollowersCount--;
            }
            else
            {
                userFollow = new UserFollow
                {
                    FollowerId = followingId,
                    FollowedId = followedId
                };
                await _set.AddAsync(userFollow);
                follower.FollowingCount++;
                followed.FollowersCount++;
            }
            _context.Users.Update(follower);
            _context.Users.Update(followed);

            await _context.SaveChangesAsync();
        }
        public async Task<bool> IsFollowingAsync(string followerId, string followedId)
        {
            return await _context.userFollows
                .AnyAsync(uf => uf.FollowerId == followerId && uf.FollowedId == followedId);
        }
    }
}
