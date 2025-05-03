using Social_Media_Application.Common.DTOs;

namespace Social_Media_Application.Hubs
{
    public interface IChatHub
    {
        Task ReceiveMessage(MessageDTO message);
        Task MessageEdited(MessageDTO message);
        Task MessageDeleted(object Content);
        Task UserIsTyping(string name);
        Task NotifyOnline(string userId, int conversationId);
        Task NotifyOffline(string userId, int conversationId);
    }
}
