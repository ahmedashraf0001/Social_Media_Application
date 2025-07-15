using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Social_Media_Application.Common.Entities
{
    public enum MediaType
    {
        Video,
        Image,
        None
    }
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [StringLength(2000)]
        public string Content { get; set; }
        public string? MediaUrl { get; set; }
        public MediaType? MediaType { get; set; }

        [Range(0, int.MaxValue)]
        public int LikesCount { get; set; }
        [Range(0, int.MaxValue)]

        public int CommentsCount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }

        public ICollection<Comment>? Comments { get; set; }
        public ICollection<PostLike>? Likes { get; set; }

    }

}
