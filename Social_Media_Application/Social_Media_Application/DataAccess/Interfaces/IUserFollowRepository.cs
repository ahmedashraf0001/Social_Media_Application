using Social_Media_Application.Common.Entities;

namespace Social_Media_Application.DataAccess.Interfaces
{
    public interface IUserFollowRepository: IRepository<UserFollow>
    {
        Task<List<UserFollow>> GetFollowersAsync(string userId, int pageNumber, int pageSize = 12);
        Task<List<UserFollow>> GetFollowedAsync(string userId, int pageNumber, int pageSize = 12);
        Task ToggleFollowAsync(string followingId, string followedId);
        Task<bool> IsFollowingAsync(string followerId, string followedId);
    }
}
