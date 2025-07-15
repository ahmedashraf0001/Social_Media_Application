namespace Social_Media_Application.Common.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string UserName { get; set; }
        public string? UserImageUrl { get; set; }
    }
}