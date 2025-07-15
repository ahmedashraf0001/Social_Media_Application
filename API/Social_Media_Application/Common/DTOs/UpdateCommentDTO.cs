using System.ComponentModel.DataAnnotations;

namespace Social_Media_Application.Common.DTOs
{
    public class UpdateCommentDTO
    {
        [Required]
        public string Content { get; set; }
    }
}
