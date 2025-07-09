using Social_Media_Application.Common.DTOs;
using Social_Media_Application.Common.Entities;
using Social_Media_Application.Common.Utils.Queries;

namespace Social_Media_Application.DataAccess.Interfaces
{
    public interface IConversationRepository : IRepository<Conversation>
    {
        Task<Conversation?> GetConversationByIdAsync(int conversationId, ConversationQueryOptions Options);
        Task<List<Conversation>> GetUserConversationsAsync(string userId, ConversationQueryOptions Options);
        Task<Conversation?> GetConversationBetweenUsersAsync(string user1Id, string user2Id, ConversationQueryOptions Options);
        Task<List<Conversation>> SearchConversationsAsync(ConversationSearchQuery searchQuery, ConversationQueryOptions options);
        Task<bool> LastMessageReadCheck(string userId, int convoId);
        Task<int> GetNumOfUnreadMessages(int convoId, string userId);
    }
}
