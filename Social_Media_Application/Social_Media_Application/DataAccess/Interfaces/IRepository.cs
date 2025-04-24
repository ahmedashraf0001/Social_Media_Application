using Social_Media_Application.Common.Utils;

namespace Social_Media_Application.DataAccess.Interfaces
{
    public interface IRepository<T>
    {
        Task<T?> GetByIdAsync(object id);
        Task<List<T>> GetAllAsync(UserQueryOptions options);
        Task AddAsync(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task SaveChangesAsync();
    }
}
