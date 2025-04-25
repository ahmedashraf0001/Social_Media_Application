using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Social_Media_Application.BusinessLogic.Services;
using Social_Media_Application.Common.DTOs;
using Social_Media_Application.Common.Entities;
using Social_Media_Application.Common.Utils;
using System.Security.Claims;

namespace Social_Media_Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }

        /// <summary>
        /// Creates a new post.
        /// </summary>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Post>> Create([FromBody] PostDTO post, IFormFile file)
        {
            try
            {
                var model = await _postService.CreatePostAsync(post, file);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        /// <summary>
        /// Deletes a post.
        /// </summary>
        [HttpDelete("{postId:int}")]
        [Authorize]
        public async Task<ActionResult> Delete(int postId)
        {
            try
            {
                await _postService.DeletePostAsync(postId);
                return Ok("Delete Succeeded!");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        /// <summary>
        /// Fetches the feed of posts for a user.
        /// </summary>
        [HttpGet("{userId:alpha}/{pageNumber:int}/{pageSize:int}")]
        [Authorize]
        public async Task<ActionResult<List<PostDTO>>> Feed(string userId, int pageNumber, int pageSize = 12)
        {
            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var model = await _postService.GenerateFeedAsync(currentUserId, userId, pageNumber, pageSize);
                return Ok(model);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        /// <summary>
        /// Fetches a post by its ID.
        /// </summary>
        [HttpGet("{postId:int}")]
        [Authorize]
        public async Task<ActionResult<PostDTO>> GetById(int postId, [FromQuery] PostQueryOptions options)
        {
            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var model = await _postService.GetPostByIdAsync(currentUserId, postId, options);
                return Ok(model);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        /// <summary>
        /// Fetches posts by a specific user.
        /// </summary>
        [HttpGet("{userId:alpha}")]
        [Authorize]
        public async Task<ActionResult<List<PostDTO>>> GetByUserId(string userId, [FromQuery] PostQueryOptions options)
        {
            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var model = await _postService.GetPostsByUserIdAsync(currentUserId, userId, options);
                return Ok(model);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        /// <summary>
        /// Updates an existing post.
        /// </summary>
        [HttpPut("{postId:int}")]
        [Authorize]
        public async Task<ActionResult> UpdatePostAsync(int postId, [FromBody] PostDTO postDTO, IFormFile file)
        {
            try
            {
                await _postService.UpdatePostAsync(postId, postDTO, file);
                return Ok("Update Succeeded!");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        /// <summary>
        /// Toggles like for a post.
        /// </summary>
        [HttpPut("{postId:int}/{userId:alpha}")]
        [Authorize]
        public async Task<ActionResult> ToggleLike(int postId, string userId)
        {
            try
            {
                bool isLiked = await _postService.ToggleLikeAsync(postId, userId);

                string message = isLiked ? "Post liked successfully!" : "Post unliked successfully!";
                return Ok(new { Message = message, IsLiked = isLiked });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }
    }
}
