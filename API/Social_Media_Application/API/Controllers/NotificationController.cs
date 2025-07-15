using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Social_Media_Application.BusinessLogic.Interfaces;
using Social_Media_Application.BusinessLogic.Services;
using Social_Media_Application.Common.DTOs;
using Social_Media_Application.Common.Utils.Queries;
using System.Security.Claims;

namespace Social_Media_Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService notificationService;

        public NotificationController(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }
        [HttpGet("Sender/{fromUserId}")]
        public async Task<ActionResult<List<NotiDTO>>> GetSender(string fromUserId) 
        {
            try
            {
                var model = await notificationService.GetNotificationsBySenderAsync(fromUserId);
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
        [HttpGet("Receiver")]
        public async Task<ActionResult<List<NotiDTO>>> GetReceiver()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                    return Unauthorized();
                var model = await notificationService.GetNotificationsByReceiverAsync(userId);
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
        [HttpGet("Unread")]
        public async Task<ActionResult<List<NotiDTO>>> GetUnreadForUser()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                    return Unauthorized();
                var model = await notificationService.GetUnreadNotificationsForUserAsync(userId);
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
        [HttpGet("UnreadCount")]
        public async Task<ActionResult<List<NotiDTO>>> GetUnreadCountForUser()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                    return Unauthorized();
                var model = await notificationService.GetUnreadNotificationCountAsync(userId);
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
        [HttpPut("MarkAsRead/{notificationId}")]
        public async Task<ActionResult<List<NotiDTO>>> MarkAsRead(int notificationId)
        {
            try
            {
                await notificationService.MarkAsReadAsync(notificationId);
                return Ok("Marked Successfully");
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
        [HttpPost("Create")]
        public async Task<ActionResult<List<NotiDTO>>> Create(NotiUpdateDTO notification)
        {
            try
            {
                await notificationService.CreateNotification(notification);
                return Ok("Created Successfully");
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
        [HttpPut("update")]
        public async Task<ActionResult<List<NotiDTO>>> Update(NotiUpdateDTO notification)
        {
            try
            {
                await notificationService.UpdateNotification(notification);
                return Ok("Updated Successfully");
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
        [HttpDelete("delete/{notificationId}")]
        public async Task<ActionResult<List<NotiDTO>>> Delete(int notificationId)
        {
            try
            {
                await notificationService.DeleteNotification(notificationId);
                return Ok("Deleted Successfully");
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
