using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Social_Media_Application.BusinessLogic.Interfaces;
using Social_Media_Application.Common.DTOs;
using Social_Media_Application.Common.Utils;
using Social_Media_Application.Common.Utils.Queries;
using System.Security.Claims;

namespace Social_Media_Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserProfileDTO>> GetCurrentUser()
        {
            try
            {
                string? currentUserId = User.Identity?.IsAuthenticated == true
                                      ? User.FindFirstValue(ClaimTypes.NameIdentifier)
                                      : null;
                var options = new UserQueryOptions { WithPosts = false };
                var userProfile = await _userService.GetCurrentUser(currentUserId, options);
                return Ok(userProfile);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserProfileDTO>> GetUserProfile(string userId)
        {
            try
            {
                string? currentUserId = User.Identity?.IsAuthenticated == true
                    ? User.FindFirstValue(ClaimTypes.NameIdentifier)
                    : null;

                var options = new UserQueryOptions { WithPosts = true };
                var userProfile = await _userService.GetUserProfileAsync(currentUserId, userId, options);
                return Ok(userProfile);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("profile")]
        [Authorize]
        public async Task<IActionResult> UpdateUserProfile([FromForm] UpdateUserProfileDTO updateUserProfileDTO)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _userService.UpdateUserProfileAsync(userId, updateUserProfileDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("follow/{userId}")]
        [Authorize]
        public async Task<IActionResult> ToggleFollow(string userId)
        {
            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (currentUserId == userId)
                    return BadRequest("You cannot follow yourself");

                await _userService.ToggleFollowAsync(currentUserId, userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{userId}/followers")]
        public async Task<ActionResult<List<UserProfileDTO>>> GetFollowers(
            string userId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 12)
        {
            try
            {
                string? currentUserId = User.Identity?.IsAuthenticated == true
                    ? User.FindFirstValue(ClaimTypes.NameIdentifier)
                    : null;

                var followers = await _userService.GetFollowersAsync(currentUserId, userId, pageNumber, pageSize);
                return Ok(followers);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{userId}/following")]
        public async Task<ActionResult<List<UserProfileDTO>>> GetFollowing(
            string userId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 12)
        {
            try
            {
                string? currentUserId = User.Identity?.IsAuthenticated == true
                    ? User.FindFirstValue(ClaimTypes.NameIdentifier)
                    : null;

                var following = await _userService.GetFollowedAsync(currentUserId, userId, pageNumber, pageSize);
                return Ok(following);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<UserProfileDTO>>> SearchUsers(
            [FromQuery] string query,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 12)
        {
            try
            {
                string? currentUserId = User.Identity?.IsAuthenticated == true
                    ? User.FindFirstValue(ClaimTypes.NameIdentifier)
                    : null;

                var users = await _userService.SearchUsersAsync(currentUserId, query, pageNumber, pageSize);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteUserAccount()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _userService.DeleteUserProfileAsync(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
