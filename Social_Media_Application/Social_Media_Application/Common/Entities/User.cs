using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Social_Media_Application.Common.Entities
{
    public class User : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [StringLength(200)]
        public string? Bio { get; set; }
        public string? PhotoUrl { get; set; }
        public int FollowersCount { get; set; } = 0;
        public int FollowingCount { get; set; } = 0;

        public ICollection<Post>? Posts { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<PostLike>? LikedPosts { get; set; }

        public ICollection<UserFollow>? Followers { get; set; }
        public ICollection<UserFollow>? Following { get; set; }
    }
}
