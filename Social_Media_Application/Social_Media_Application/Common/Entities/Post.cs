using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media_Application.Common.Entities
{
    public enum MediaType
    {
        Video,
        Image
    }
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [StringLength(500)]
        public string Content { get; set; }
        public string? MediaUrl { get; set; }
        public MediaType? MediaType { get; set; }
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }

        public ICollection<Comment>? Comments { get; set; }
        public ICollection<PostLike>? Likes { get; set; }

    }

}