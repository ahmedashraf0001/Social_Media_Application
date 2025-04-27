using Microsoft.AspNetCore.Identity;
using Social_Media_Application.Common.DTOs;
using Social_Media_Application.Common.Utils;

namespace Social_Media_Application.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<UserProfileDTO> GetUserProfileAsync(string? currentUserId, string userId, UserQueryOptions options);
        Task UpdateUserProfileAsync(string userID, UpdateUserProfileDTO updateUserProfileDTO);
        Task DeleteUserProfileAsync(string userId);
        Task ToggleFollowAsync(string followingId, string followedId);
        Task<List<UserProfileDTO>> GetFollowersAsync(string? currentUserId, string userId, int pageNumber, int pageSize = 12);
        Task<List<UserProfileDTO>> GetFollowedAsync(string? currentUserId, string userId, int pageNumber, int pageSize = 12);
        Task<List<UserProfileDTO>> SearchUsersAsync(string? currentUserId, string searchTerm, int pageNumber, int pageSize = 12);
    }
}
