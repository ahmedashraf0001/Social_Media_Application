using Social_Media_Application.Common.Entities;

namespace Social_Media_Application.Common.Utils.Queries
{
    public class PostSearchQuery
    {
        public string? Keyword { get; set; }
        public string? UserId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public MediaType? MediaType { get; set; }
    }

}
