namespace Social_Media_Application.Common.Entities
{
    public class UserFollow
    {
        public int FollowerId { get; set; }
        public User Follower { get; set; }

        public int FollowedId { get; set; }
        public User Followed { get; set; }
    }
}
