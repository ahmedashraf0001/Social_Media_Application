using Microsoft.EntityFrameworkCore;
using Social_Media_Application.BusinessLogic.Interfaces;
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
        public async Task<string> ToggleFollowAsync(string followerId, string followedId)
        {
            var userFollow = await _set.FirstOrDefaultAsync(uf =>
                uf.FollowerId == followerId && uf.FollowedId == followedId);

            var follower = await _context.Users.FindAsync(followerId);
            var followed = await _context.Users.FindAsync(followedId);

            if (follower == null || followed == null)
                throw new Exception("One or both users not found");

            if (userFollow != null)
            {
                _set.Remove(userFollow);
                follower.FollowingCount = Math.Max(follower.FollowingCount - 1, 0);
                followed.FollowersCount = Math.Max(followed.FollowersCount - 1, 0);
                await _context.SaveChangesAsync();
                return "UnFollowed";
            }

            await _set.AddAsync(new UserFollow
            {
                FollowerId = followerId,
                FollowedId = followedId
            });

            var existingConversation = await _context.conversations.FirstOrDefaultAsync(c =>
                (c.CurrentUserId == followerId && c.otherUserId == followedId) ||
                (c.CurrentUserId == followedId && c.otherUserId == followerId));

            if (existingConversation == null)
            {
                await _context.conversations.AddAsync(new Conversation
                {
                    CurrentUserId = followerId,
                    otherUserId = followedId,
                    CreatedAt = DateTime.UtcNow,
                    LastMessageAt = DateTime.UtcNow,
                    LastMessageContent = "Start New Conversation"
                });
            }

            follower.FollowingCount++;
            followed.FollowersCount++;

            _context.Users.Update(follower);
            _context.Users.Update(followed);

            await _context.SaveChangesAsync();
            return "Followed";
        }

        public async Task<bool> IsFollowingAsync(string followerId, string followedId)
        {
            return await _context.userFollows
                .AnyAsync(uf => uf.FollowerId == followerId && uf.FollowedId == followedId);
        }
    }
}
