using Microsoft.AspNetCore.Identity;
using Social_Media_Application.Common.DTOs;
using Social_Media_Application.Common.Entities;
using Social_Media_Application.Common.Utils.Queries;

namespace Social_Media_Application.BusinessLogic.Interfaces
{
    public interface IPostService
    {
        Task<PostDTO> CreatePostAsync(string currentUserId, PostCreateDTO postDTO);
        Task<PostDTO> GetPostByIdAsync(string currentUserId, int postId, PostQueryOptions options);
        Task<List<PostDTO>> GetPostsByUserIdAsync(string currentUserId, string userId, PostQueryOptions options);
        Task UpdatePostAsync(PostUpdateDTO postDTO);
        Task DeletePostAsync(int postId);
        Task<bool> ToggleLikeAsync(int postId, string userId);
        Task<List<PostDTO>> GenerateFeedAsync(string currentUserId,  PostQueryOptions options);
        Task<List<UserLikeDTO>> GetPostLikesAsync(int postId, PostQueryOptions options);

        Task<List<PostDTO>> SearchPostsAsync(string currentUserId, PostSearchQuery searchQuery, PostQueryOptions options);
    }
}
