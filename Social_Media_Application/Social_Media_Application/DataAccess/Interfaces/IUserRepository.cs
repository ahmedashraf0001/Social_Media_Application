using Social_Media_Application.Common.Entities;
using Social_Media_Application.Common.Utils;

namespace Social_Media_Application.DataAccess.Interfaces
{
    public interface IUserRepository: IRepository<User>
    {
        Task<User> GetUserAsync(string Id, UserQueryOptions options);
        Task<User?> GetByUsernameAsync(string username, UserQueryOptions options);

        Task DeleteUserAsync(string userId);
    }
}
