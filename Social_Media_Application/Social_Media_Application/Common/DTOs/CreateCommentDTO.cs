using System.ComponentModel.DataAnnotations;

namespace Social_Media_Application.Common.DTOs
{
    public class CreateCommentDTO
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public int PostId { get; set; }
    }
}
