namespace Social_Media_Application.Common.Utils
{
    public class PostQueryOptions
    {
        public bool IncludeLikedUsers { get; set; } = false;
        public bool IncludeComments { get; set; } = false;
        public bool IncludeAuthorDetails { get; set; } = false;
    }
}
