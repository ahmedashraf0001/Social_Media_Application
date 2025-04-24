using Microsoft.AspNetCore.Identity;
using Social_Media_Application.BusinessLogic.Interfaces;
using Social_Media_Application.Common.DTOs;

namespace Social_Media_Application.BusinessLogic.Services
{
    public class CommentService : ICommentService
    {
        public Task<CommentDTO> AddCommentAsync(CommentDTO commentDTO)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteCommentAsync(int commentId)
        {
            throw new NotImplementedException();
        }

        public Task<List<CommentDTO>> GetCommentsByPostIdAsync(int postId)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateCommentAsync(int commentId, CommentDTO commentDTO)
        {
            throw new NotImplementedException();
        }
    }
}
