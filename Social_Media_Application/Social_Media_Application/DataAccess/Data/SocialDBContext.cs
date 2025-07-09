using E_Commerce_Platform.EF;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Social_Media_Application.Common.Entities;
using System.Reflection.Emit;

namespace Social_Media_Application.DataAccess.Data
{
    public class SocialDBContext: IdentityDbContext<User>
    {
        private readonly IConfiguration configuration;

        public DbSet<User> users { get; set; }
        public DbSet<Post> posts {  get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<PostLike> postLikes { get; set; }
        public DbSet<UserFollow> userFollows { get; set; }
        public DbSet<Conversation> conversations { get; set; }
        public DbSet<Message> messages { get; set; }
        public DbSet<Notification> notifications { get; set; }

        public SocialDBContext()
        { }
        public SocialDBContext(DbContextOptions<SocialDBContext> options, IConfiguration _configuration) :base(options)
        {
            configuration = _configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=SocialDB;Integrated Security=True;Encrypt=False;Trust Server Certificate=True");
            }

            optionsBuilder.ConfigureWarnings(warnings =>
              warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PostLike>()
                   .HasKey(pl => new { pl.PostId, pl.UserId });

            builder.Entity<UserFollow>()
                   .HasKey(uf => new { uf.FollowerId, uf.FollowedId });

            builder.Entity<UserFollow>()
                .HasOne(uf => uf.Follower)
                .WithMany(u => u.Following)
                .HasForeignKey(uf => uf.FollowerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserFollow>()
                .HasOne(uf => uf.Followed)
                .WithMany(u => u.Followers)
                .HasForeignKey(uf => uf.FollowedId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.Entity<PostLike>()
                .HasOne(e => e.Post)
                .WithMany(u => u.Likes)
                .HasForeignKey(e => e.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<PostLike>()
                .HasOne(e => e.User)
                .WithMany(u => u.LikedPosts)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Conversation>()
                .HasOne(c => c.CurrentUser)
                .WithMany(u => u.ConversationsInitiated)
                .HasForeignKey(c => c.CurrentUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Conversation>()
                .HasOne(c => c.OtherUser)
                .WithMany(u => u.ConversationsReceived)
                .HasForeignKey(c => c.otherUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(m => m.Conversation)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ConversationId)
                .OnDelete(DeleteBehavior.Cascade); 

            builder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.NoAction); 

            builder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Notification>()
                .HasOne(n => n.ToUser)
                .WithMany(u => u.ReceivedNotifications)
                .HasForeignKey(n => n.ToUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Notification>()
                .HasOne(n => n.FromUser)
                .WithMany(u => u.SentNotifications)
                .HasForeignKey(n => n.FromUserId)
                .OnDelete(DeleteBehavior.Restrict);


            DatabaseSeeder.Seed(builder);
        }
    }
}
