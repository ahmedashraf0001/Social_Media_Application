namespace Social_Media_Application.Common.Entities
{
    public enum MediaType
    {
        Image,
        Video
    }
    public class Media
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public MediaType Type { get; set; } 
        public int PostId { get; set; }
        public Post Post { get; set; }
    }

}
