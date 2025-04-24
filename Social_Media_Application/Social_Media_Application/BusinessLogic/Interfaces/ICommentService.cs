using Microsoft.AspNetCore.Identity;
using Social_Media_Application.Common.DTOs;

namespace Social_Media_Application.BusinessLogic.Interfaces
{
    public interface ICommentService
    {
        Task<CommentDTO> AddCommentAsync(CommentDTO commentDTO);
        Task<IdentityResult> UpdateCommentAsync(int commentId, CommentDTO commentDTO);
        Task<IdentityResult> DeleteCommentAsync(int commentId);
        Task<List<CommentDTO>> GetCommentsByPostIdAsync(int postId);
    }
}
