using Microsoft.AspNetCore.Identity;
using Social_Media_Application.BusinessLogic.Interfaces;
using Social_Media_Application.Common.DTOs;
using Social_Media_Application.Common.Entities;
using Social_Media_Application.Common.Utils.Queries;
using Social_Media_Application.DataAccess.Interfaces;
using Social_Media_Application.DataAccess.Repositories;

namespace Social_Media_Application.BusinessLogic.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IConversationRepository _conversationRepository;
        private readonly IConversationService _conversationService;
        public MessageService(IMessageRepository messageRepository, IConversationRepository conversationRepository, IConversationService conversationService)
        {
            _messageRepository = messageRepository;
            _conversationRepository = conversationRepository;
            _conversationService = conversationService;
        }
        public async Task DeleteMessageAsync(int messageId)
        {
            var result = await _messageRepository.GetMessageByIdAsync(messageId, new MessageQueryOptions());
            if (result == null)
            {
                throw new InvalidOperationException("No Messages found");
            }
            await _messageRepository.DeleteAsync(result);
        }

        public async Task<MessageDTO?> EditMessageAsync(int messageId, string content)
        {
            Message message = await _messageRepository.GetMessageByIdAsync(messageId, new MessageQueryOptions());
            var convo = await _conversationRepository.GetByIdAsync(message.ConversationId);
            message.Content = content;
            message.IsEdited = true;
            convo.LastMessageAt = DateTime.Now;
            convo.LastMessageContent = $"Edited: {content}";        
            await _messageRepository.SaveChangesAsync();
            return await MapToDTOAsync(message);
        }

        public async Task<MessageDTO?> GetMessageByIdAsync(int messageId)
        {
            var result = await _messageRepository.GetMessageByIdAsync(messageId, new MessageQueryOptions ());

            if (result == null)
            {
                throw new InvalidOperationException("No Messages found");
            }

            return await MapToDTOAsync(result);
        }
        public Task<MessageDTO> MapToDTOAsync(Message message)
        {
            var messageDTOs = new MessageDTO()
            {
                Id = message.Id,
                ConversationId = message.ConversationId, 
                SenderId = message.SenderId,
                ReceiverId = message.ReceiverId,
                Content = message.Content,
                IsRead = message.IsRead,
                SentAt = message.SentAt,
                IsDeleted = message.IsDeleted,
                IsEdited = message.IsEdited, 
            };

            return Task.FromResult(messageDTOs); 
        }

        public async Task<List<MessageDTO>> GetUnreadMessagesForUserAsync(string userId)
        {
            var result = await _messageRepository.GetUnreadMessagesForUserAsync(userId, new MessageQueryOptions());

            if (result == null)
            {
                throw new InvalidOperationException("No Unread Messages found");
            }

            List<MessageDTO> model = new List<MessageDTO>();

            foreach (var message in result)
            {
                model.Add(await MapToDTOAsync(message));
            }

            return model;
        }
        public async Task MarkMessageAsReadAsync(int messageId)
        {
            await _messageRepository.MarkMessageAsReadAsync(messageId); 
        }
        public async Task<MessageDTO> SendMessageAsync(MessageCreateDTO messageDTO, string senderId)
        {
            var result = await _conversationRepository.GetConversationBetweenUsersAsync(
                senderId, messageDTO.ReceiverId, new ConversationQueryOptions());


            if (result == null)
            {
                result = await _conversationService.CreateConversationAsync(
                    new ConversationCreateDTO { 
                        CurrentUserId = senderId,
                        OtherUserId = messageDTO.ReceiverId,
                        LastMessageContent = messageDTO.Content,
                        LastMessageAt = DateTime.UtcNow
                    });
            }

            var message = new Message()
            {
                ConversationId = result.Id,
                Content = messageDTO.Content,
                SenderId = senderId,
                ReceiverId = messageDTO.ReceiverId,
                SentAt = DateTime.UtcNow
            };
            result.LastMessageAt = DateTime.UtcNow;
            result.LastMessageContent = message.Content;

            await _messageRepository.SendMessageAsync(message);

            await _messageRepository.SaveChangesAsync();

            return await MapToDTOAsync(message);
        }

        public async Task<List<MessageDTO>> SearchMessagesAsync(MessageQueryOptions options, MessageSearchQuery searchQuery)
        {
            var result = await _messageRepository.SearchMessagesAsync(searchQuery, options);

            if (result == null)
            {
                throw new InvalidOperationException("No Messages found");
            }

            List<MessageDTO> model = new List<MessageDTO>();

            foreach (var message in result)
            {
                model.Add(await MapToDTOAsync(message));
            }

            return model;
        }
    }
}
