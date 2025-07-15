using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media_Application.Common.Entities
{
    public class Conversation
    {
        [Key]
        public int Id { get; set; }
        public string CurrentUserId { get; set; }       
        public string otherUserId { get; set; }    
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? LastMessageAt { get; set; }
        public string? LastMessageContent { get; set; } = string.Empty;

        [ForeignKey(nameof(CurrentUserId))]
        public User? CurrentUser { get; set; }

        [ForeignKey(nameof(otherUserId))]
        public User? OtherUser { get; set; }

        public List<Message>? Messages { get; set; } = new List<Message>(); 
    }
}
