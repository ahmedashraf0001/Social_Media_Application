namespace Social_Media_Application.Common.Utils
{
    public class PostQueryOptions
    {
        public bool WithLikedPosts { get; set; } = false;
        public bool WithComments { get; set; } = false;
        public bool WithUsers { get; set; } = false;
    }

}
