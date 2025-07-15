namespace Social_Media_Application.Common.DTOs
{
    public class NotiUpdateDTO
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public int? PostId { get; set; }
    }
    public class NotiMsg
    {
        public string Message { get; set; }
        public string UserImage { get; set; }
        public int? PostId { get; set; }
    }
}
