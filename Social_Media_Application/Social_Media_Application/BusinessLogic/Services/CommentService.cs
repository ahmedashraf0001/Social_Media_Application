using Microsoft.AspNetCore.Identity;
using Social_Media_Application.BusinessLogic.Interfaces;
using Social_Media_Application.Common.DTOs;
using Social_Media_Application.Common.Entities;
using Social_Media_Application.Common.Utils;
using Social_Media_Application.Common.Utils.Queries;
using Social_Media_Application.DataAccess.Interfaces;

namespace Social_Media_Application.BusinessLogic.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<CommentDTO> AddCommentAsync(string userId, CreateCommentDTO createCommentDTO)
        {
            if (createCommentDTO == null)
                throw new ApplicationException( "Comment data cannot be null");

            if (string.IsNullOrEmpty(createCommentDTO.Content))
                throw new ApplicationException("Comment content cannot be empty");

            if (createCommentDTO.PostId <= 0)
                throw new ApplicationException("Invalid post ID");

            if (string.IsNullOrEmpty(userId))
                throw new ApplicationException("User ID is required");

            var comment = new Comment
            {
                Text = createCommentDTO.Content,
                PostId = createCommentDTO.PostId,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
            };

            await _commentRepository.AddAsync(comment);

            return MapToDTO(comment);
        }

        public async Task UpdateCommentAsync(int commentId, UpdateCommentDTO updateCommentDTO)
        {
            if (commentId <= 0)
                throw new ApplicationException("Invalid comment ID");

            if (string.IsNullOrEmpty(updateCommentDTO.Content))
                throw new ApplicationException("Comment content cannot be empty");

            var comment = await _commentRepository.GetByIdAsync(commentId);

            if (comment == null)
                throw new KeyNotFoundException($"Comment with ID {commentId} not found.");

            comment.Text = updateCommentDTO.Content;

            try
            {
                await _commentRepository.UpdateAsync(comment);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to update comment: {ex.Message}");
            }
        }

        public async Task DeleteCommentAsync(int commentId)
        {
            if (commentId <= 0)
                throw new ArgumentException("Invalid comment ID");

            var comment = await _commentRepository.GetByIdAsync(commentId);

            if (comment == null)
                throw new KeyNotFoundException($"Comment with ID {commentId} not found.");

            try
            {
                await _commentRepository.DeleteAsync(comment);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to delete comment: {ex.Message}");
            }
        }

        public async Task<List<CommentDTO>> GetCommentsByPostIdAsync(int postId)
        {
            if (postId <= 0)
                throw new ArgumentException("Invalid post ID");

            var options = new CommentQueryOptions { WithUsers = true };
            var comments = await _commentRepository.GetByPostIdAsync(postId, options);

            var commentDTOs = new List<CommentDTO>();
            foreach (var comment in comments)
            {
                commentDTOs.Add(MapToDTO(comment));
            }

            return commentDTOs;
        }

        private CommentDTO MapToDTO(Comment comment)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment), "Cannot map null comment");

            return new CommentDTO
            {
                Id = comment.Id,
                Content = comment.Text,
                PostId = comment.PostId,
                UserId = comment.UserId,
                CreatedAt = comment.CreatedAt,
                UserName = comment.User?.FirstName + " " + comment.User?.LastName,
                UserImageUrl = comment.User?.PhotoUrl
            };
        }
    }
}
