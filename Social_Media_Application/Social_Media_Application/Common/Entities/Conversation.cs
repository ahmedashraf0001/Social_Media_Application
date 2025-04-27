using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media_Application.Common.Entities
{
    public class Conversation
    {
        [Key]
        public int Id { get; set; } 

        public string User1Id { get; set; } 
        public string User2Id { get; set; } 

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? LastMessageAt { get; set; }
        public string? LastMessageContent { get; set; } = string.Empty;

        [ForeignKey(nameof(User1Id))]
        public User? User1 { get; set; }

        [ForeignKey(nameof(User2Id))]
        public User? User2 { get; set; }

        public List<Message>? Messages { get; set; } = new List<Message>(); 
    }
}
