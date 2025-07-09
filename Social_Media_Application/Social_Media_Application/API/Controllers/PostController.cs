using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Social_Media_Application.BusinessLogic.Interfaces;
using Social_Media_Application.BusinessLogic.Services;
using Social_Media_Application.Common.DTOs;
using Social_Media_Application.Common.Entities;
using Social_Media_Application.Common.Utils.Queries;
using System.Security.Claims;

namespace Social_Media_Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost("Create/")]
        [Authorize]
        public async Task<ActionResult<Post>> Create([FromForm] PostCreateDTO post)
        {
            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var model = await _postService.CreatePostAsync(currentUserId, post);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpDelete("Delete/{postId:int}")]
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

        [HttpGet("Feed/{pageNumber:int}/{pageSize:int}")]
        [Authorize]
        public async Task<ActionResult<List<PostDTO>>> Feed(int pageNumber = 1, int pageSize = 12)
        {
            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var model = await _postService.GenerateFeedAsync(currentUserId, new PostQueryOptions() { PageNumber = pageNumber, PageSize = pageSize, IncludeAuthorDetails = true });
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

        [HttpGet("Id/{postId:int}")]
        [Authorize]
        public async Task<ActionResult<PostDTO>> GetById(int postId)
        {
            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var model = await _postService.GetPostByIdAsync(currentUserId, postId, new PostQueryOptions());
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

        [HttpGet("User/{userId}/{pageNumber:int}/{pageSize:int}")]
        [Authorize]
        public async Task<ActionResult<List<PostDTO>>> GetByUserId(string userId, int pageNumber = 1, int pageSize = 12)
        {
            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var model = await _postService.GetPostsByUserIdAsync(currentUserId, userId, new PostQueryOptions() { PageNumber = pageNumber, PageSize = pageSize });
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

        [HttpPut("Update/")]
        [Authorize]
        public async Task<ActionResult> UpdatePostAsync([FromForm] PostUpdateDTO postDTO)
        {
            try
            {
                await _postService.UpdatePostAsync(postDTO);
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

        [HttpPut("Like/{postId:int}")]
        [Authorize]
        public async Task<ActionResult> ToggleLike(int postId)
        {
            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                bool isLiked = await _postService.ToggleLikeAsync(postId, currentUserId);

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
        [HttpGet("Search/")]
        public async Task<ActionResult<List<PostDTO>>> SearchPostsAsync(
            [FromQuery] PostSearchQuery searchQuery, 
            [FromQuery] int pageNumber = 1,         
            [FromQuery] int pageSize = 12           
        )
        {
            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var model = await _postService.SearchPostsAsync(currentUserId, searchQuery ,new PostQueryOptions());
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

        [HttpGet("{postId:int}/Likes/{pageNumber:int}/{pageSize:int}")]
        [Authorize]
        public async Task<ActionResult<List<UserLikeDTO>>> GetUsersWhoLikedPostAsync(int postId, int pageNumber = 1, int pageSize = 12)
        {
            try
            {
                var model = await _postService.GetPostLikesAsync(postId, new PostQueryOptions() { PageNumber = pageNumber, PageSize = pageSize });
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
    }
}
    
