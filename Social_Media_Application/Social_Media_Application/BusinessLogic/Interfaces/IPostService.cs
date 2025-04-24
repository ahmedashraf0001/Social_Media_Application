using Microsoft.AspNetCore.Identity;
using Social_Media_Application.Common.DTOs;

namespace Social_Media_Application.BusinessLogic.Interfaces
{
    public interface IPostService
    {
        Task<PostDTO> CreatePostAsync(PostDTO postDTO);
        Task<PostDTO> GetPostByIdAsync(int postId);
        Task<List<PostDTO>> GetPostsByUserIdAsync(string userId);
        Task<IdentityResult> UpdatePostAsync(int postId, PostDTO postDTO);
        Task<IdentityResult> DeletePostAsync(int postId);
        //it should generate feeds based on user following and sort them with the most liked, recent date
        Task<List<PostDTO>> GenerateFeed(string userId, int pageNumber, int pageSize = 12);
    }
}
