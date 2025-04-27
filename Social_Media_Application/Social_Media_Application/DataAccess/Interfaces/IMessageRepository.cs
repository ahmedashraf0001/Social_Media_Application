using Social_Media_Application.Common.DTOs;
using Social_Media_Application.Common.Entities;

namespace Social_Media_Application.DataAccess.Interfaces
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<Message?> GetMessageByIdAsync(int messageId, MessageQueryOptions options);

        Task<List<Message>> GetMessagesByConversationIdAsync(int conversationId, MessageQueryOptions options);

        Task<List<Message>> GetUnreadMessagesForUserAsync(string userId, MessageQueryOptions options);

        Task<Message> SendMessageAsync(Message message);

        Task MarkMessageAsReadAsync(int messageId);

    }
}
