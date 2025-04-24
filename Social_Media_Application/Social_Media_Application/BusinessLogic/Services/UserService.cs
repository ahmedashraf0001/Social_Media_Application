using Microsoft.AspNetCore.Identity;
using Social_Media_Application.BusinessLogic.Interfaces;
using Social_Media_Application.Common.DTOs;
using Social_Media_Application.Common.Utils;

namespace Social_Media_Application.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        public Task<IdentityResult> DeleteUserProfileAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> FollowUserAsync(string followingId, string followedId)
        {
            throw new NotImplementedException();
        }

        public Task<UserProfileDTO> GetUserProfileAsync(string userId, UserQueryOptions options)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UnfollowUserAsync(string followingId, string followedId)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateUserProfileAsync(string userID, UserProfileDTO userProfileDTO)
        {
            throw new NotImplementedException();
        }
    }
}
