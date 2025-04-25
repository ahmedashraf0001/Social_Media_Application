using Microsoft.EntityFrameworkCore;
using Social_Media_Application.Common.Utils;
using Social_Media_Application.DataAccess.Data;
using Social_Media_Application.DataAccess.Interfaces;
using System.Threading.Tasks;

namespace Social_Media_Application.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly SocialDBContext _context;
        public readonly DbSet<T> _set;
        public Repository(SocialDBContext context)
        {
            _context = context;
            _set = context.Set<T>();
        }
        public virtual async Task AddAsync(T entity)
        {
            await _set.AddAsync(entity);
            await SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _set.Remove(entity);
            await SaveChangesAsync();
        }

        public virtual async Task<List<T>> GetAllAsync(object options)
        {
            return await _set.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(object id)
        {
            return await _set.FindAsync(id);
        }

        public virtual async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await SaveChangesAsync();
        }
    }
}
