namespace Social_Media_Application.Common.DTOs
{
    public class NotiDTO
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string userImage { get; set; }
        public int? PostId { get; set; }
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; } = false;
    }

}
