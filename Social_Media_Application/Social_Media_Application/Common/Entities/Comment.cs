using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media_Application.Common.Entities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int PostId { get; set; }
        [ForeignKey(nameof(PostId))]
        public Post? Post { get; set; }

        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
    }
}
