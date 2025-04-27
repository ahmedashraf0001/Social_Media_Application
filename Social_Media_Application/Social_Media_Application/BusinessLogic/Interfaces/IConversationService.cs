using Social_Media_Application.Common.DTOs;
using Social_Media_Application.Common.Entities;
using Social_Media_Application.Common.Utils;

namespace Social_Media_Application.BusinessLogic.Interfaces
{
    public interface IConversationService
    {
        Task<ConversationDTO?> GetConversationAsync(int conversationId, string CurrentUserId);
        Task<List<ConversationDTO>> GetUserConversationsAsync(string CurrentUserId);
        Task<ConversationDTO> CreateConversationAsync(ConversationCreateDTO conversationDTO, string CurrentUserId);
        Task DeleteConversationAsync(int conversationId);
        Task<ConversationDTO?> GetConversationBetweenUsersAsync(string CurrentUserId, string user2Id);
    }
}
