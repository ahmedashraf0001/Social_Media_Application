namespace Social_Media_Application.Common.Entities
{
    public class Message
    {
        public int Id { get; set; }

        public int ConversationId { get; set; }

        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; } = false;
        public User? Sender { get; set; }
        public User? Receiver { get; set; }

        public Conversation? Conversation { get; set; }
    }
}
