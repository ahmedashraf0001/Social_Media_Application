using Social_Media_Application.Common.Entities;
using Social_Media_Application.Common.Utils.Queries;

namespace Social_Media_Application.DataAccess.Interfaces
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<List<Comment>> GetByPostIdAsync(int postId, CommentQueryOptions options);
        Task<List<Comment>> GetByUserIdAsync(string userId, CommentQueryOptions options);
        Task UpdateCommentCounter(int postId, bool increment);
    }
}
