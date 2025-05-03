namespace Social_Media_Application.Common.DTOs
{
    public class ConversationDTO
    {
        public int Id { get; set; } 

        public string CurrentUserId { get; set; }
        public string OtherUserId { get; set; }

        public DateTime CreatedAt { get; set; }
        public string? LastMessageContent { get; set; }
        public DateTime? LastMessageAt { get; set; }

        public string? ConversationName { get; set; } 
        public string? PhotoUrl { get; set; }
        public List<MessageDTO>? Messages { get; set; }
    }
    public class ConversationCreateDTO
    {
        public string CurrentUserId { get; set; }
        public string OtherUserId { get; set; }
        public DateTime? LastMessageAt { get; set; }
        public string? LastMessageContent { get; set; }

    }
}
