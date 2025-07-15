namespace Social_Media_Application.Common.Utils.Queries
{
    public class ConversationSearchQuery
    {
        public string? CurrentUserId { get; set; }
        public string? OtherUserId { get; set; }  
        public string? Keyword { get; set; } 
        public DateTime? StartDate { get; set; }  
        public DateTime? EndDate { get; set; }  
    }
}
