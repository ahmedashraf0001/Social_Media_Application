using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media_Application.Common.Entities
{
    public class UserFollow
    {
        public string FollowerId { get; set; }
        [ForeignKey(nameof(FollowerId))]
        public User? Follower { get; set; }

        public string FollowedId { get; set; }
        [ForeignKey(nameof(FollowedId))]
        public User? Followed { get; set; }
    }
}
