using Microsoft.AspNetCore.Identity;
using Social_Media_Application.BusinessLogic.Interfaces;
using Social_Media_Application.Common.DTOs;

namespace Social_Media_Application.BusinessLogic.Services
{
    public class PostService : IPostService
    {
        public Task<PostDTO> CreatePostAsync(PostDTO postDTO)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeletePostAsync(int postId)
        {
            throw new NotImplementedException();
        }

        public Task<List<PostDTO>> GenerateFeed(string userId, int pageNumber, int pageSize = 12)
        {
            throw new NotImplementedException();
        }

        public Task<PostDTO> GetPostByIdAsync(int postId)
        {
            throw new NotImplementedException();
        }

        public Task<List<PostDTO>> GetPostsByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdatePostAsync(int postId, PostDTO postDTO)
        {
            throw new NotImplementedException();
        }
    }
}
