using Microsoft.AspNetCore.Identity;
using Social_Media_Application.Common.DTOs;

namespace Social_Media_Application.BusinessLogic.Interfaces
{
    public interface ICommentService
    {
        Task<CommentDTO> AddCommentAsync(string userId, CreateCommentDTO createCommentDTO);
        Task UpdateCommentAsync(int commentId, UpdateCommentDTO updateCommentDTO);
        Task DeleteCommentAsync(int commentId);
        Task<List<CommentDTO>> GetCommentsByPostIdAsync(int postId);
    }
}
