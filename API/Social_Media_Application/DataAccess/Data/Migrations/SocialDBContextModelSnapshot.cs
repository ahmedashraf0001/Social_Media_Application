﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Social_Media_Application.DataAccess.Data;

#nullable disable

namespace Social_Media_Application.Migrations
{
    [DbContext(typeof(SocialDBContext))]
    partial class SocialDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Social_Media_Application.Common.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("comments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2025, 7, 8, 21, 36, 17, 807, DateTimeKind.Utc).AddTicks(5337),
                            PostId = 1,
                            Text = "Great post, John!",
                            UserId = "2"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2025, 7, 8, 21, 36, 17, 807, DateTimeKind.Utc).AddTicks(5934),
                            PostId = 3,
                            Text = "Love this, Alex!",
                            UserId = "1"
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2025, 7, 8, 21, 36, 17, 807, DateTimeKind.Utc).AddTicks(5936),
                            PostId = 2,
                            Text = "Amazing content, Jane!",
                            UserId = "3"
                        });
                });

            modelBuilder.Entity("Social_Media_Application.Common.Entities.Conversation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CurrentUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("LastMessageAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastMessageContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("otherUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CurrentUserId");

                    b.HasIndex("otherUserId");

                    b.ToTable("conversations");
                });

            modelBuilder.Entity("Social_Media_Application.Common.Entities.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ConversationId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsEdited")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<string>("ReceiverId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SenderId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("SentAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ConversationId");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("messages");
                });

            modelBuilder.Entity("Social_Media_Application.Common.Entities.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("FromUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PostId")
                        .HasColumnType("int");

                    b.Property<string>("ToUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("userImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FromUserId");

                    b.HasIndex("ToUserId");

                    b.ToTable("notifications");
                });

            modelBuilder.Entity("Social_Media_Application.Common.Entities.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CommentsCount")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("LikesCount")
                        .HasColumnType("int");

                    b.Property<int?>("MediaType")
                        .HasColumnType("int");

                    b.Property<string>("MediaUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("posts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CommentsCount = 0,
                            Content = "This is John's first post!",
                            CreatedAt = new DateTime(2025, 7, 8, 21, 36, 17, 807, DateTimeKind.Utc).AddTicks(4166),
                            LikesCount = 0,
                            MediaUrl = "/images/post1.jpg",
                            UserId = "1"
                        },
                        new
                        {
                            Id = 2,
                            CommentsCount = 0,
                            Content = "Jane's first post!",
                            CreatedAt = new DateTime(2025, 7, 8, 21, 36, 17, 807, DateTimeKind.Utc).AddTicks(4803),
                            LikesCount = 0,
                            MediaUrl = "/images/post2.jpg",
                            UserId = "2"
                        },
                        new
                        {
                            Id = 3,
                            CommentsCount = 0,
                            Content = "Hello, I'm Alex! Here's my first post.",
                            CreatedAt = new DateTime(2025, 7, 8, 21, 36, 17, 807, DateTimeKind.Utc).AddTicks(4805),
                            LikesCount = 0,
                            MediaUrl = "/images/post3.jpg",
                            UserId = "3"
                        });
                });

            modelBuilder.Entity("Social_Media_Application.Common.Entities.PostLike", b =>
                {
                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("PostId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("postLikes");

                    b.HasData(
                        new
                        {
                            PostId = 1,
                            UserId = "2"
                        },
                        new
                        {
                            PostId = 2,
                            UserId = "3"
                        },
                        new
                        {
                            PostId = 3,
                            UserId = "1"
                        });
                });

            modelBuilder.Entity("Social_Media_Application.Common.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Bio")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FollowersCount")
                        .HasColumnType("int");

                    b.Property<int>("FollowingCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("JoinedIn")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondaryPhotoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "1",
                            AccessFailedCount = 0,
                            Bio = "Hello, I am John!",
                            ConcurrencyStamp = "dc914efb-ff9e-4ec5-b4f5-bb031d89159e",
                            Email = "john@example.com",
                            EmailConfirmed = false,
                            FirstName = "john",
                            FollowersCount = 0,
                            FollowingCount = 0,
                            JoinedIn = new DateTime(2025, 7, 9, 0, 36, 17, 804, DateTimeKind.Local).AddTicks(5971),
                            LastName = "doe",
                            LockoutEnabled = false,
                            PhoneNumberConfirmed = false,
                            PhotoUrl = "/images/john.jpg",
                            SecurityStamp = "6b74009e-820a-4901-a5d3-bde473c3407d",
                            TwoFactorEnabled = false,
                            UserName = "john_doe"
                        },
                        new
                        {
                            Id = "2",
                            AccessFailedCount = 0,
                            Bio = "Hey, I am Jane!",
                            ConcurrencyStamp = "9a658ac9-1bef-403e-8d18-b4dee118bd52",
                            Email = "jane@example.com",
                            EmailConfirmed = false,
                            FirstName = "jane",
                            FollowersCount = 0,
                            FollowingCount = 0,
                            JoinedIn = new DateTime(2025, 7, 9, 0, 36, 17, 806, DateTimeKind.Local).AddTicks(9631),
                            LastName = "smith",
                            LockoutEnabled = false,
                            PhoneNumberConfirmed = false,
                            PhotoUrl = "/images/jane.jpg",
                            SecurityStamp = "f61234a0-ec42-48bc-8cb8-b3210c4c5e62",
                            TwoFactorEnabled = false,
                            UserName = "jane_smith"
                        },
                        new
                        {
                            Id = "3",
                            AccessFailedCount = 0,
                            Bio = "Alex here, love to share posts!",
                            ConcurrencyStamp = "1812a24b-b0b3-4180-b3e1-2109c0708a3d",
                            Email = "alex@example.com",
                            EmailConfirmed = false,
                            FirstName = "alex",
                            FollowersCount = 0,
                            FollowingCount = 0,
                            JoinedIn = new DateTime(2025, 7, 9, 0, 36, 17, 806, DateTimeKind.Local).AddTicks(9664),
                            LastName = "king",
                            LockoutEnabled = false,
                            PhoneNumberConfirmed = false,
                            PhotoUrl = "/images/alex.jpg",
                            SecurityStamp = "949caa64-7e12-4077-a894-029d6ea3d703",
                            TwoFactorEnabled = false,
                            UserName = "alex_king"
                        });
                });

            modelBuilder.Entity("Social_Media_Application.Common.Entities.UserFollow", b =>
                {
                    b.Property<string>("FollowerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FollowedId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("FollowerId", "FollowedId");

                    b.HasIndex("FollowedId");

                    b.ToTable("userFollows");

                    b.HasData(
                        new
                        {
                            FollowerId = "1",
                            FollowedId = "2"
                        },
                        new
                        {
                            FollowerId = "2",
                            FollowedId = "3"
                        },
                        new
                        {
                            FollowerId = "3",
                            FollowedId = "1"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Social_Media_Application.Common.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Social_Media_Application.Common.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Social_Media_Application.Common.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Social_Media_Application.Common.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Social_Media_Application.Common.Entities.Comment", b =>
                {
                    b.HasOne("Social_Media_Application.Common.Entities.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Social_Media_Application.Common.Entities.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Social_Media_Application.Common.Entities.Conversation", b =>
                {
                    b.HasOne("Social_Media_Application.Common.Entities.User", "CurrentUser")
                        .WithMany("ConversationsInitiated")
                        .HasForeignKey("CurrentUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Social_Media_Application.Common.Entities.User", "OtherUser")
                        .WithMany("ConversationsReceived")
                        .HasForeignKey("otherUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CurrentUser");

                    b.Navigation("OtherUser");
                });

            modelBuilder.Entity("Social_Media_Application.Common.Entities.Message", b =>
                {
                    b.HasOne("Social_Media_Application.Common.Entities.Conversation", "Conversation")
                        .WithMany("Messages")
                        .HasForeignKey("ConversationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Social_Media_Application.Common.Entities.User", "Receiver")
                        .WithMany("ReceivedMessages")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Social_Media_Application.Common.Entities.User", "Sender")
                        .WithMany("SentMessages")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Conversation");

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Social_Media_Application.Common.Entities.Notification", b =>
                {
                    b.HasOne("Social_Media_Application.Common.Entities.User", "FromUser")
                        .WithMany("SentNotifications")
                        .HasForeignKey("FromUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Social_Media_Application.Common.Entities.User", "ToUser")
                        .WithMany("ReceivedNotifications")
                        .HasForeignKey("ToUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FromUser");

                    b.Navigation("ToUser");
                });

            modelBuilder.Entity("Social_Media_Application.Common.Entities.Post", b =>
                {
                    b.HasOne("Social_Media_Application.Common.Entities.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Social_Media_Application.Common.Entities.PostLike", b =>
                {
                    b.HasOne("Social_Media_Application.Common.Entities.Post", "Post")
                        .WithMany("Likes")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Social_Media_Application.Common.Entities.User", "User")
                        .WithMany("LikedPosts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Social_Media_Application.Common.Entities.UserFollow", b =>
                {
                    b.HasOne("Social_Media_Application.Common.Entities.User", "Followed")
                        .WithMany("Followers")
                        .HasForeignKey("FollowedId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Social_Media_Application.Common.Entities.User", "Follower")
                        .WithMany("Following")
                        .HasForeignKey("FollowerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Followed");

                    b.Navigation("Follower");
                });

            modelBuilder.Entity("Social_Media_Application.Common.Entities.Conversation", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("Social_Media_Application.Common.Entities.Post", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Likes");
                });

            modelBuilder.Entity("Social_Media_Application.Common.Entities.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("ConversationsInitiated");

                    b.Navigation("ConversationsReceived");

                    b.Navigation("Followers");

                    b.Navigation("Following");

                    b.Navigation("LikedPosts");

                    b.Navigation("Posts");

                    b.Navigation("ReceivedMessages");

                    b.Navigation("ReceivedNotifications");

                    b.Navigation("SentMessages");

                    b.Navigation("SentNotifications");
                });
#pragma warning restore 612, 618
        }
    }
}
