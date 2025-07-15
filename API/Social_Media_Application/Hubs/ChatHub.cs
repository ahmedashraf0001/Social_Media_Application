using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualBasic;
using Social_Media_Application.BusinessLogic.Interfaces;
using Social_Media_Application.Common.DTOs;
using Social_Media_Application.Common.Entities;
using Social_Media_Application.DataAccess.Interfaces;

namespace Social_Media_Application.Hubs
{
    public class ChatHub:Hub<IChatHub>
    {
        private readonly IConversationService _conversationSerivce;
        private readonly IMessageService _messageService;
        private readonly ConnectionMapping _connectionMapping;
        private readonly IUserRepository _userRepository;
        private readonly IHubContext<ChatHub, IChatHub> _hubContext;

        public ChatHub(IConversationService conversationSerivce, ConnectionMapping connectionMapping,
                       IMessageService messageService, IUserRepository userRepository, IHubContext<ChatHub, IChatHub> hubContext)
        {
            _conversationSerivce = conversationSerivce;
            _connectionMapping = connectionMapping;
            _messageService = messageService;
            _userRepository = userRepository;
            _hubContext = hubContext;
        }
        public override async Task OnConnectedAsync()
        {
            try
            {
                var userId = Context.UserIdentifier;

                if (string.IsNullOrEmpty(userId)) return;

                var isFirstConnection = !_connectionMapping.IsConnected(userId);

                _connectionMapping.Add(userId, Context.ConnectionId);

                var conversations = await _conversationSerivce.GetUserConversationsAsync(
                    userId, new Common.Utils.Queries.ConversationQueryOptions());

                foreach (var conversation in conversations)
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, conversation.Id.ToString());

                    if (isFirstConnection)
                    {
                        await Clients.OthersInGroup(conversation.Id.ToString())
                                     .NotifyOnline(userId, conversation.Id);
                    }
                }

                var onlineUsers = _connectionMapping.GetAllConnections().Where(id => id != userId).ToList();

                var onlineConversations = conversations
                    .Where(e => onlineUsers.Contains(e.CurrentUserId) || onlineUsers.Contains(e.OtherUserId))
                    .Select(e => new
                    {
                        userId = e.CurrentUserId == userId ? e.OtherUserId : e.CurrentUserId,
                        convoId = e.Id
                    });
                await base.OnConnectedAsync();
                await Task.Delay(100);
                foreach (var convo in onlineConversations)
                {
                    await Clients.Caller.NotifyOnline(convo.userId, convo.convoId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Connecting: {ex.Message}");
            }

            
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            try
            {
                var userId = Context.UserIdentifier;

                if (string.IsNullOrEmpty(userId)) return;

                _connectionMapping.Remove(userId, Context.ConnectionId);

                var isLastConnection = !_connectionMapping.IsConnected(userId);

                var conversations = await _conversationSerivce.GetUserConversationsAsync(
                    userId, new Common.Utils.Queries.ConversationQueryOptions());

                foreach (var conversation in conversations)
                {
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, conversation.Id.ToString());

                    if (isLastConnection)
                    {
                        await Clients.OthersInGroup(conversation.Id.ToString())
                                     .NotifyOffline(userId, conversation.Id);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Disconnecting: {ex.Message}");
            }

            await base.OnDisconnectedAsync(exception);
        }
        public async Task SendMessage(MessageCreateDTO message)
        {
            try
            {
                var userId = Context.UserIdentifier;

                if (string.IsNullOrEmpty(userId)) return;

                MessageDTO messageDTO = await _messageService.SendMessageAsync(message, userId);

                if (messageDTO == null) return;

                var conversation = await _conversationSerivce.GetConversationBetweenUsersAsync(
                    userId, message.ReceiverId, new Common.Utils.Queries.ConversationQueryOptions());

                await Clients.Group(conversation.Id.ToString()).ReceiveMessage(messageDTO);
            }
            catch (InvalidOperationException ex)
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}");
            }
        }
        public async Task EditMessage(int messageId, string content)
        {
            try
            {
                var userId = Context.UserIdentifier;

                if (string.IsNullOrEmpty(userId)) return;

                MessageDTO messageDTO = await _messageService.EditMessageAsync(messageId, content);

                if (messageDTO == null || messageDTO.SenderId != userId) return;

                await Clients.Group(messageDTO.ConversationId.ToString()).MessageEdited(messageDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error editing message: {ex.Message}");
            }
        }
        public async Task MarkMessageAsRead(int messageId)
        {
            try
            {
                var userId = Context.UserIdentifier;
                if (string.IsNullOrEmpty(userId)) return;

                var message = await _messageService.GetMessageByIdAsync(messageId);
                if (message == null || message.ReceiverId != userId) return;

                await _messageService.MarkMessageAsReadAsync(messageId);
                await Clients.Group(message.ConversationId.ToString()).MessageMarkedAsRead(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Marking message as read: {ex.Message}");
            }
        }
        public async Task SendNotification(string toUserId, string message)
        {
            await _hubContext.Clients.User(toUserId).ReceiveNotification(message);
        }
        public async Task DeleteMessage(int messageId)
        {
            try
            {
                var userId = Context.UserIdentifier;

                if (string.IsNullOrEmpty(userId)) return;

                var message = await _messageService.GetMessageByIdAsync(messageId);

                if (message == null || message.SenderId != userId) return;

                await Clients.Group(message.ConversationId.ToString()).MessageDeleted(new { messageId, content = "This message was deleted" });

                await _messageService.EditMessageAsync(messageId, "This message was deleted");
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error deleting message with ID {messageId}: {ex.Message}");
            }
        }
        public async Task UserTyping(int conversationId, string name)
        {
            try
            {
                var userId = Context.UserIdentifier;

                if (string.IsNullOrEmpty(userId)) return;

                await Clients.OthersInGroup(conversationId.ToString()).UserIsTyping(name);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reporting usertyping: {ex.Message}");
            }
        }
    }
}
