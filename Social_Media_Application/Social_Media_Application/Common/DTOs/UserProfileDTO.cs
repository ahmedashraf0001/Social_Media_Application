namespace Social_Media_Application.Common.DTOs
{
    public class UserProfileDTO
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime JoinedIn { get; set; }
        public string Location { get; set; }
        public string? Bio { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? SecondaryPictureUrl { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingCount { get; set; }
        public bool IsFollowedByCurrentUser { get; set; }
        public List<PostDTO>? Posts { get; set; }
    }
}