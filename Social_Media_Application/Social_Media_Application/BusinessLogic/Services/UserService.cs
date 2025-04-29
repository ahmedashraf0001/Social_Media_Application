using Microsoft.AspNetCore.Identity;
using Social_Media_Application.BusinessLogic.Interfaces;
using Social_Media_Application.Common.DTOs;
using Social_Media_Application.Common.Entities;
using Social_Media_Application.Common.Utils.Queries;
using Social_Media_Application.DataAccess.Interfaces;

namespace Social_Media_Application.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserFollowRepository _userFollowRepository;
        private readonly IMediaService _mediaService;
        public UserService(IUserRepository userRepository, IUserFollowRepository userFollowRepository, IMediaService mediaService)
        {
            _userRepository = userRepository;
            _userFollowRepository = userFollowRepository;
            _mediaService = mediaService;
        }

        public async Task DeleteUserProfileAsync(string userId)
        {
            await _userRepository.DeleteUserAsync(userId);
        }
        public async Task ToggleFollowAsync(string followingId, string followedId)
        {
            await _userFollowRepository.ToggleFollowAsync(followingId, followedId);
        }
        public async Task<UserProfileDTO> GetUserProfileAsync(string? currentUserId, string userId, UserQueryOptions options)
        {
            var user = await _userRepository.GetUserAsync(userId, options);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            bool isFollowing = false;
            if (!string.IsNullOrEmpty(currentUserId) && userId != currentUserId)
            {
                isFollowing = await _userFollowRepository.IsFollowingAsync(currentUserId, userId);
            }

            var userProfile = new UserProfileDTO
            {
                Id = user.Id,
                Username = user.UserName,
                ProfilePictureUrl = user.PhotoUrl,
                FirstName = user.FirstName,
                LastName = user.LastName,
                FollowersCount = user.FollowersCount,
                FollowingCount = user.FollowingCount,
                IsFollowedByCurrentUser = isFollowing
            };
            if (options.WithPosts && user.Posts != null && user.Posts.Any())
            {
                userProfile.Posts = user.Posts.Select(p => new PostDTO
                {
                    Id = p.Id,
                    Content = p.Content,
                    MediaUrl = p.MediaUrl,
                    MediaType = p.MediaType,
                    CreatedAt = p.CreatedAt,
                    UserId = p.UserId,
                    AuthorUsername = user.UserName,
                    LikeCount = p.LikesCount,
                    CommentCount = p.CommentsCount,
                    
                    IsLikedByCurrentUser = !string.IsNullOrEmpty(currentUserId) &&
                                           p.Likes != null &&
                                           p.Likes.Any(l => l.UserId == currentUserId)
                }).ToList();
            }
            return userProfile;
        }
        public async Task<List<UserProfileDTO>> GetFollowersAsync(string? currentUserId, string userId, int pageNumber, int pageSize = 12)
        {
            var followers = await _userFollowRepository.GetFollowersAsync(userId, pageNumber, pageSize);
            var userDtos = new List<UserProfileDTO>();

            foreach (var user in followers)
            {
                bool isFollowing = false;
                if (!string.IsNullOrEmpty(currentUserId) && user.Follower.Id != currentUserId)
                {
                    isFollowing = await _userFollowRepository.IsFollowingAsync(currentUserId, user.Follower.Id);
                }

                userDtos.Add(new UserProfileDTO
                {
                    Id = user.Follower.Id,
                    Username = user.Follower.UserName,
                    ProfilePictureUrl = user.Follower.PhotoUrl,
                    FirstName = user.Follower.FirstName,
                    LastName = user.Follower.LastName,
                    FollowersCount = user.Follower.FollowersCount,
                    FollowingCount = user.Follower.FollowingCount,
                    IsFollowedByCurrentUser = isFollowing
                });
            }
            return userDtos;
        }
        public async Task<List<UserProfileDTO>> GetFollowedAsync(string? currentUserId, string userId, int pageNumber, int pageSize = 12)
        {
            var followedUsers = await _userFollowRepository.GetFollowedAsync(userId, pageNumber, pageSize);
            var userDtos = new List<UserProfileDTO>();

            foreach (var user in followedUsers)
            {
                bool isFollowing = false;
                if (!string.IsNullOrEmpty(currentUserId) && user.Followed.Id != currentUserId)
                {
                    isFollowing = await _userFollowRepository.IsFollowingAsync(currentUserId, user.Followed.Id);
                }

                userDtos.Add(new UserProfileDTO
                {
                    Id = user.Followed.Id,
                    Username = user.Followed.UserName,
                    ProfilePictureUrl = user.Followed.PhotoUrl,
                    FirstName = user.Followed.FirstName,
                    LastName = user.Followed.LastName,
                    FollowersCount = user.Followed.FollowersCount,
                    FollowingCount = user.Followed.FollowingCount,
                    IsFollowedByCurrentUser = isFollowing
                });
            }
            return userDtos;
        }
        public async Task UpdateUserProfileAsync(string userId, UpdateUserProfileDTO updateUserProfileDTO)
        {
            var user = await _userRepository.GetUserAsync(userId, new UserQueryOptions());

            if (user == null)
                throw new Exception("User not found");

            if (!string.IsNullOrEmpty(updateUserProfileDTO.FirstName))
                user.FirstName = updateUserProfileDTO.FirstName;

            if (!string.IsNullOrEmpty(updateUserProfileDTO.LastName))
                user.LastName = updateUserProfileDTO.LastName;
            if (updateUserProfileDTO.ProfilePicture != null)
            {
                if (!string.IsNullOrEmpty(user.PhotoUrl))
                {
                    await _mediaService.DeleteMediaAsync(user.PhotoUrl);
                }
                var (photoUrl, _) = await _mediaService.UploadMediaAsync(updateUserProfileDTO.ProfilePicture);
                user.PhotoUrl = photoUrl;
            }
            await _userRepository.UpdateAsync(user);
        }
        public async Task<List<UserProfileDTO>> SearchUsersAsync(string? currentUserId, string searchTerm, int pageNumber, int pageSize = 12)
        {
            var users = await _userRepository.SearchUsersAsync(searchTerm, pageNumber, pageSize);
            var userDtos = new List<UserProfileDTO>();

            foreach (var user in users)
            {
                bool isFollowing = false;
                if (!string.IsNullOrEmpty(currentUserId) && user.Id != currentUserId)
                {
                    isFollowing = await _userFollowRepository.IsFollowingAsync(currentUserId, user.Id);
                }

                userDtos.Add(new UserProfileDTO
                {
                    Id = user.Id,
                    Username = user.UserName,
                    ProfilePictureUrl = user.PhotoUrl,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    FollowersCount = user.FollowersCount,
                    FollowingCount = user.FollowingCount,
                    IsFollowedByCurrentUser = isFollowing
                });
            }
            return userDtos;
        }
    }
}
