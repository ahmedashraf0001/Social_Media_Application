namespace Social_Media_Application.DataAccess.Interfaces
{
    public interface IRepository<T>
    {
        Task<T?> GetByIdAsync(object id);
        Task<List<T>> GetAllAsync(object options);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task SaveChangesAsync();
    }
}
