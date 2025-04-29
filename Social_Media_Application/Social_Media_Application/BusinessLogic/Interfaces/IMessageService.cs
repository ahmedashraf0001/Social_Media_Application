using Social_Media_Application.Common.DTOs;
using Social_Media_Application.Common.Entities;
using Social_Media_Application.Common.Utils.Queries;
using Social_Media_Application.DataAccess.Interfaces;

namespace Social_Media_Application.BusinessLogic.Interfaces
{
    public interface IMessageService
    {
        Task<MessageDTO?> GetMessageByIdAsync(int messageId);

        Task<List<MessageDTO>> GetUnreadMessagesForUserAsync(string userId);

        Task<MessageDTO> SendMessageAsync(MessageCreateDTO messageDTO, string senderId);

        Task MarkMessageAsReadAsync(int messageId);

        Task DeleteMessageAsync(int messageId);

        Task<MessageDTO?> EditMessageAsync (int messageId, string content);

        Task<List<MessageDTO>> SearchMessagesAsync(MessageQueryOptions options, MessageSearchQuery searchQuery);
    }
}
