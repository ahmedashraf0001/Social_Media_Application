using Microsoft.EntityFrameworkCore;
using Social_Media_Application.Common.Entities;

namespace E_Commerce_Platform.EF
{
    public static class DatabaseSeeder
    {
        private static readonly DateTime SeedBaseDate = new DateTime(2024, 1, 1);

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
              new User { Id = "1", UserName = "john_doe", FirstName = "john", LastName = "doe", Email = "john@example.com", Bio = "Hello, I am John!", PhotoUrl = "/images/john.jpg" },
              new User { Id = "2", UserName = "jane_smith", FirstName = "jane", LastName = "smith", Email = "jane@example.com", Bio = "Hey, I am Jane!", PhotoUrl = "/images/jane.jpg" },
              new User { Id = "3", UserName = "alex_king", FirstName = "alex", LastName = "king", Email = "alex@example.com", Bio = "Alex here, love to share posts!", PhotoUrl = "/images/alex.jpg" }
          );

            // Seed Posts
            modelBuilder.Entity<Post>().HasData(
                new Post { Id = 1, Content = "This is John's first post!", MediaUrl = "/images/post1.jpg", UserId = "1" },
                new Post { Id = 2, Content = "Jane's first post!", MediaUrl = "/images/post2.jpg", UserId = "2" },
                new Post { Id = 3, Content = "Hello, I'm Alex! Here's my first post.", MediaUrl = "/images/post3.jpg", UserId = "3" }
            );

            // Seed Comments
            modelBuilder.Entity<Comment>().HasData(
                new Comment { Id = 1, Text = "Great post, John!", PostId = 1, UserId = "2" },
                new Comment { Id = 2, Text = "Love this, Alex!", PostId = 3, UserId = "1" },
                new Comment { Id = 3, Text = "Amazing content, Jane!", PostId = 2, UserId = "3" }
            );

            // Seed PostLikes
            modelBuilder.Entity<PostLike>().HasData(
                new PostLike { PostId = 1, UserId = "2" }, // Jane likes John's post
                new PostLike { PostId = 2, UserId = "3" }, // Alex likes Jane's post
                new PostLike { PostId = 3, UserId = "1" }  // John likes Alex's post
            );

            // Seed UserFollows (following relationships)
            modelBuilder.Entity<UserFollow>().HasData(
                new UserFollow { FollowerId = "1", FollowedId = "2" }, // John follows Jane
                new UserFollow { FollowerId = "2", FollowedId = "3" }, // Jane follows Alex
                new UserFollow { FollowerId = "3", FollowedId = "1" }  // Alex follows John
            );
        }
    }
}