using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Social_Media_Application.BusinessLogic.Interfaces;
using Social_Media_Application.Common.DTOs;
using Social_Media_Application.Common.Entities;
using System.Security.Claims;

namespace Social_Media_Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessagingController : ControllerBase
    {
        private readonly IConversationService _conversationService;
        private readonly IMessageService _messageService;
        public MessagingController(IConversationService conversationService, IMessageService messageService)
        {
            _conversationService = conversationService;
            _messageService = messageService;
        }
        
        [HttpGet("conversations/{conversationId:int}")]
        public async Task<ActionResult<ConversationDTO?>> GetConversationById(int conversationId)
        {
            try
            {
                var Current = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var conversations = await _conversationService.GetConversationAsync(conversationId, Current);

                if (conversations == null)
                {
                    return NoContent();
                }

                return Ok(conversations);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error." });
            }
        }
        
        [HttpGet("conversations/user/{userId}")]
        public async Task<ActionResult<List<ConversationDTO>>> GetAllConversations(string userId)
        {
            try
            {
                var conversations = await _conversationService.GetUserConversationsAsync(userId);

                if (conversations == null || !conversations.Any())
                {
                    return NoContent();
                }

                return Ok(conversations);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error." });
            }
        }
        
        [HttpGet("conversations/current")]
        public async Task<ActionResult<List<ConversationDTO>>> GenerateCurrentUserConversations()
        {
            try
            {
                var Current = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var conversation = await _conversationService.GetUserConversationsAsync(Current);

                if (conversation == null)
                {
                    return NoContent();
                }

                return Ok(conversation);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error." });
            }
        }
        
        [HttpGet("conversations/between/{user2Id}")]
        public async Task<ActionResult<ConversationDTO?>> GetMessagesBetweenUsers(string user2Id)
        {
            try
            {
                var Current = User.FindFirstValue(ClaimTypes.NameIdentifier);       

                var conversation = await _conversationService.GetConversationBetweenUsersAsync(Current, user2Id);

                if (conversation == null)
                {
                    return NoContent();
                }

                return Ok(conversation);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error." });
            }
        }
        
        [HttpPost("conversations")]
        public async Task<ActionResult<ConversationDTO?>> CreateConversation([FromBody] ConversationCreateDTO conversationDTO)
        {
            try
            {
                var Current = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var conversations = await _conversationService.CreateConversationAsync(conversationDTO, Current);

                if (conversations == null)
                {
                    return NoContent();
                }

                return Ok(conversations);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error." });
            }
        }

        [HttpDelete("conversations/{conversationId:int}")]
        public async Task<ActionResult> DeleteConversation(int conversationId)
        {
            try
            {
                await _conversationService.DeleteConversationAsync(conversationId);

                return Ok("Deleted Successfully!");
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error." });
            }
        }
        
        [HttpGet("messages/{messageId}")]
        public async Task<ActionResult<MessageDTO?>> GetMessageByIdAsync(int messageId)
        {
            try
            {
                var message = await _messageService.GetMessageByIdAsync(messageId);

                if (message == null)
                {
                    return NoContent();
                }

                return Ok(message);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error." });
            }
        }
        
        [HttpGet("messages/unread/{userId}")]
        public async Task<ActionResult<List<MessageDTO>>> GetUnreadMessagesForUserAsync(string userId)
        {
            try
            {
                var message = await _messageService.GetUnreadMessagesForUserAsync(userId);

                if (message == null)
                {
                    return NoContent();
                }

                return Ok(message);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error." });
            }
        }

        [HttpPost("messages/")]
        public async Task<ActionResult<MessageDTO>> SendMessageAsync([FromBody] MessageCreateDTO messageDTO)
        {
            try
            {
                var Current = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var message = await _messageService.SendMessageAsync(messageDTO, Current);

                if (message == null)
                {
                    return NoContent();
                }

                return Ok(message);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error." });
            }
        }
        
        [HttpDelete("messages/{messageId}")]
        public async Task<ActionResult> DeleteMessageAsync(int messageId)
        {
            try
            {
                await _messageService.DeleteMessageAsync(messageId);

                return Ok("Deleted Successfully!");

            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error." });
            }
        }
        
        [HttpPut("messages/{messageId}/{content}")]
        public async Task<ActionResult<MessageDTO?>> EditMessageAsync(int messageId, string content)
        {
            try
            {
               var message = await _messageService.EditMessageAsync(messageId, content);

                if (message == null)
                {
                    return NoContent();
                }

                return Ok(message);

            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error.", detail = ex.Message });
            }
        }
    }
}
