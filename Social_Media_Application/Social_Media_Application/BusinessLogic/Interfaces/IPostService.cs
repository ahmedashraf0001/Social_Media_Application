using Microsoft.AspNetCore.Identity;
using Social_Media_Application.Common.DTOs;
using Social_Media_Application.Common.Entities;
using Social_Media_Application.Common.Utils;

namespace Social_Media_Application.BusinessLogic.Interfaces
{
    public interface IPostService
    {
        Task<Post> CreatePostAsync(PostDTO postDTO, IFormFile file);
        Task<PostDTO> GetPostByIdAsync(string currentUserId, int postId, PostQueryOptions options);
        Task<List<PostDTO>> GetPostsByUserIdAsync(string currentUserId, string userId, PostQueryOptions options);
        Task UpdatePostAsync(int postId, PostDTO postDTO, IFormFile file);
        Task DeletePostAsync(int postId);
        Task<bool> ToggleLikeAsync(int postId, string userId);
        Task<List<PostDTO>> GenerateFeedAsync(string currentUserId, string userId, int pageNumber, int pageSize = 12);
    }
}
