namespace Social_Media_Application.Common.Utils.Queries
{
    public class MessageSearchQuery
    {
        public int? ConversationId { get; set; } 
        public string? SenderId { get; set; } 
        public string? RecipientId { get; set; }  
        public string? Keyword { get; set; } 
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

}
