using Social_Media_Application.Common.Entities;

namespace Social_Media_Application.DataAccess.Interfaces
{
    public interface IUserFollowRepository: IRepository<UserFollow>
    {
        Task<List<UserFollow>> GetFollowers(string userId);
        Task<List<UserFollow>> GetFollowed(string userId);
    }
}
