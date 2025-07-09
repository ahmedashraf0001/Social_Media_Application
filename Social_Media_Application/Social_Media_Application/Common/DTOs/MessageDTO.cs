namespace Social_Media_Application.Common.DTOs
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public int ConversationId { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; } = false;
        public bool IsEdited { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
    }
    public class MessageCreateDTO
    {
        public string ReceiverId { get; set; }
        public string Content { get; set; }
    }
}
