using Social_Media_Application.Common.Entities;
using Social_Media_Application.Common.Utils.Queries;

namespace Social_Media_Application.DataAccess.Interfaces
{
    public interface IUserRepository: IRepository<User>
    {
        Task<User> GetUserAsync(string Id, UserQueryOptions options);
        Task<User?> GetByUsernameAsync(string username, UserQueryOptions options);
        Task<List<User>> SearchUsersAsync(string searchTerm, int pageNumber, int pageSize = 12);
        Task<List<User>> GetAllAsync(int pageNumber, int pageSize = 12);
        Task DeleteUserAsync(string userId);
    }
}
