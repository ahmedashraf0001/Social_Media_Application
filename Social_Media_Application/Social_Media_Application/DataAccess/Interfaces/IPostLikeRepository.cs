using Social_Media_Application.Common.DTOs;
using Social_Media_Application.Common.Entities;

namespace Social_Media_Application.DataAccess.Interfaces
{
    public interface IPostLikeRepository:IRepository<PostLike>
    {
        Task<List<UserLikeDTO>> GetPostLikesWithUsersAsync(int postId, int pageNumber, int pageSize = 12);
        Task CreateLike(int postId, string userId);
        Task RemoveLike(int postId, string userId);
        Task<PostLike> GetLikeAsync(int postId, string userId);
    }
}
