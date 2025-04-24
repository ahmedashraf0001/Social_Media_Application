using Microsoft.AspNetCore.Identity;
using Social_Media_Application.Common.DTOs;
using Social_Media_Application.Common.Utils;

namespace Social_Media_Application.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<UserProfileDTO> GetUserProfileAsync(string userId, UserQueryOptions options);
        Task<IdentityResult> UpdateUserProfileAsync(string userID,UserProfileDTO userProfileDTO);
        Task<IdentityResult> DeleteUserProfileAsync(string userId);
        Task<IdentityResult> FollowUserAsync(string followingId, string followedId);
        Task<IdentityResult> UnfollowUserAsync(string followingId, string followedId);
    }
}
