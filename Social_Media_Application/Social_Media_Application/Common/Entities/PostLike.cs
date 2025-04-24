using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media_Application.Common.Entities
{
    public class PostLike
    {

        public int PostId { get; set; }
        [ForeignKey(nameof(PostId))]
        public Post? Post { get; set; }

        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
    }
}
