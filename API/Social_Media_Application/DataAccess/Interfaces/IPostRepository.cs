using Social_Media_Application.Common.DTOs;
using Social_Media_Application.Common.Entities;
using Social_Media_Application.Common.Utils.Queries;

namespace Social_Media_Application.DataAccess.Interfaces
{
    public interface IPostRepository:IRepository<Post>
    {
        Task<List<Post>> GetPostsByUserIdAsync(string userId, PostQueryOptions options);
        Task<Post> GetPostAsync(int Id, PostQueryOptions options);
        Task<List<PostDTO>> GenerateFeedAsync(string currentUserId, PostQueryOptions options);
        Task<List<Post>> SearchPostsAsync(PostSearchQuery searchQuery, PostQueryOptions queryOptions);
    }
}
