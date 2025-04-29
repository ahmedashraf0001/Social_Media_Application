using Social_Media_Application.Common.Entities;
using Social_Media_Application.Common.Validations;
using System.ComponentModel.DataAnnotations;

namespace Social_Media_Application.Common.DTOs
{
    public class PostDTO
    {
        public int Id { get; set; }                         
        public string Content { get; set; }                 
        public string? MediaUrl { get; set; }  
        public MediaType? MediaType { get; set; }
        public DateTime CreatedAt { get; set; }            
        public string UserId { get; set; }                  
        public string? AuthorUsername { get; set; }         

        public int LikeCount { get; set; }               
        public bool IsLikedByCurrentUser { get; set; }     
        public int CommentCount { get; set; }              
    }
    public class PostCreateDTO
    {
        public string Content { get; set; }

        [AllowedFileExtensions(new[] { ".png", ".jpg", ".jpeg", ".mp4", ".gif" })]
        [MaxFileSize(50 * 1024 * 1024)] 
        public IFormFile? Media { get; set; }
    }

    public class PostUpdateDTO
    {
        public int Id { get; set; }
        public string? Content { get; set; }

        [AllowedFileExtensions(new[] { ".png", ".jpg", ".jpeg", ".mp4", ".gif" })]
        [MaxFileSize(50 * 1024 * 1024)]
        public IFormFile? Media { get; set; }
    }
}