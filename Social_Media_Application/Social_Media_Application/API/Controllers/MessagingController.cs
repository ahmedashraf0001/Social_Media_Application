using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Social_Media_Application.BusinessLogic.Interfaces;
using Social_Media_Application.Common.DTOs;
using Social_Media_Application.Common.Entities;
using Social_Media_Application.Common.Utils.Queries;
using Social_Media_Application.DataAccess.Interfaces;
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
        
        [HttpGet("conversations/Id/{conversationId:int}/{pageNumber:int}/{pageSize:int}/{withMessages:bool}")]
        public async Task<ActionResult<ConversationDTO?>> GetConversationById(int conversationId,bool withMessages = false, int pageNumber = 1, int pageSize = 12)
        {
            try
            {
                var Current = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var conversations = await _conversationService.GetConversationAsync(conversationId, Current, new ConversationQueryOptions { WithMessages = withMessages, PageNumber = pageNumber , PageSize = pageSize});

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
        
        [HttpGet("conversations/All/{userId}/{pageNumber:int}/{pageSize:int}/{withMessages:bool}")]
        public async Task<ActionResult<List<ConversationDTO>>> GetAllConversations(string userId, bool withMessages = false, int pageNumber = 1, int pageSize = 12)
        {
            try
            {
                var conversations = await _conversationService.GetUserConversationsAsync(userId, new ConversationQueryOptions { PageNumber = pageNumber, PageSize = pageSize , WithMessages = withMessages });

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
        
        [HttpGet("conversations/inbox/{pageNumber:int}/{pageSize:int}")]
        public async Task<ActionResult<List<ConversationInboxDTO>>> GenerateCurrentUserInbox(int pageNumber = 1, int pageSize = 12)
        {
            try
            {
                var Current = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var result = await _conversationService.GetUserConversationsAsync(Current, new ConversationQueryOptions { PageNumber = pageNumber, PageSize = pageSize });

                var inbox = await _conversationService.GetInbox(result);

                if (inbox == null)
                {
                    return NoContent();
                }

                return Ok(inbox);
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
        
        [HttpGet("conversations/Between/{otherUserId}/{pageNumber:int}/{pageSize:int}")]
        public async Task<ActionResult<ConversationDTO?>> GetConversationBetweenUsers(string otherUserId, int pageNumber = 1, int pageSize = 12)
        {
            try
            {
                var Current = User.FindFirstValue(ClaimTypes.NameIdentifier);       

                var conversation = await _conversationService.GetConversationBetweenUsersAsync(Current, otherUserId, new ConversationQueryOptions { PageNumber = pageNumber, PageSize = pageSize, WithMessages = true });

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

        [HttpDelete("conversations/Delete/{conversationId:int}")]
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

        [HttpGet("conversations/Search")]
        public async Task<ActionResult<List<ConversationDTO>>> SearchForConverstation(
            [FromQuery] ConversationSearchQuery searchQuery,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 12,
            bool withMessages = false
        )
        {
            try
            {
                var Current = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var conversation = await _conversationService.SearchConversationsAsync( searchQuery, new ConversationQueryOptions { PageNumber = pageNumber, PageSize = pageSize, WithMessages = withMessages });

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

        [HttpGet("messages/Id/{messageId}")]
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
        
        [HttpGet("messages/Unread/{userId}")]
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
        [HttpGet("messages/Search")]
        public async Task<ActionResult<List<MessageDTO>>> SearchForMessages(
            [FromQuery] MessageSearchQuery searchQuery,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 12
        )
        {
            try
            {
                var messages = await _messageService.SearchMessagesAsync(new MessageQueryOptions { PageNumber = pageNumber, PageSize = pageSize }, searchQuery);

                if (messages == null)
                {
                    return NoContent();
                }

                return Ok(messages);
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

        [HttpPost("messages/Send")]
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
        [HttpDelete("messages/Delete/{messageId}")]
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
        
        [HttpPut("messages/Edit/{messageId}/{content}")]
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
        [HttpPut("messages/MarkAsRead/{messageId:int}")]
        public async Task<ActionResult<MessageDTO?>> MarkAsReadAsync(int messageId)
        {
            try
            {
                await _messageService.MarkMessageAsReadAsync(messageId);

                return Ok($"Message {messageId} Marked As Read!");

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
