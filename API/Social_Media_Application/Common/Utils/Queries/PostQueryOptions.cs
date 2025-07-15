namespace Social_Media_Application.Common.Utils.Queries
{
    public class PostQueryOptions
    {
        public bool IncludeLikedUsers { get; set; } = false;
        public bool IncludeComments { get; set; } = false;
        public bool IncludeAuthorDetails { get; set; } = false;

        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 12;
    }
}
