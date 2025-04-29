using Social_Media_Application.Common.DTOs;
using Social_Media_Application.Common.Entities;
using Social_Media_Application.Common.Utils.Queries;
using Social_Media_Application.DataAccess.Interfaces;

namespace Social_Media_Application.BusinessLogic.Interfaces
{
    public interface IConversationService
    {
        Task<ConversationDTO?> GetConversationAsync(int conversationId, string CurrentUserId, ConversationQueryOptions options);
        Task<List<ConversationDTO>> GetUserConversationsAsync(string CurrentUserId, ConversationQueryOptions options);
        Task<ConversationDTO> CreateConversationAsync(ConversationCreateDTO conversationDTO, string CurrentUserId);
        Task DeleteConversationAsync(int conversationId);
        Task<ConversationDTO?> GetConversationBetweenUsersAsync(string CurrentUserId, string user2Id, ConversationQueryOptions options);
        Task<List<ConversationDTO>> SearchConversationsAsync(ConversationSearchQuery searchQuery, ConversationQueryOptions options);
        Task<List<ConversationInboxDTO>> GetInbox(List<ConversationDTO> convo);
    }
}
