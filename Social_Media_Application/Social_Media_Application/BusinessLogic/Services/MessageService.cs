using Microsoft.AspNetCore.Identity;
using Social_Media_Application.BusinessLogic.Interfaces;
using Social_Media_Application.Common.DTOs;
using Social_Media_Application.Common.Entities;
using Social_Media_Application.Common.Utils;
using Social_Media_Application.DataAccess.Interfaces;
using Social_Media_Application.DataAccess.Repositories;

namespace Social_Media_Application.BusinessLogic.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IConversationRepository _conversationRepository;
        public MessageService(IMessageRepository messageRepository, IConversationRepository conversationRepository)
        {
            _messageRepository = messageRepository;
            _conversationRepository = conversationRepository;
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
            message.Content = content;
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
                ConversationId = message.Id, 
                SenderId = message.SenderId,
                ReceiverId = message.ReceiverId,
                Content = message.Content,
                IsRead = message.IsRead,
                SentAt = message.SentAt
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
            var result = await _conversationRepository.GetConversationByIdAsync(messageDTO.ConversationId, new ConversationQueryOptions());
            Message message = new Message()
            {
                ConversationId = messageDTO.ConversationId,
                Content = messageDTO.Content,
                SenderId= senderId,               
                ReceiverId= result.User2Id,
            };

            result.LastMessageAt = DateTime.UtcNow;
            result.LastMessageContent = messageDTO.Content;

            await _messageRepository.SaveChangesAsync();

            await _messageRepository.SendMessageAsync(message);
            return await MapToDTOAsync(message);
        }
    }
}
