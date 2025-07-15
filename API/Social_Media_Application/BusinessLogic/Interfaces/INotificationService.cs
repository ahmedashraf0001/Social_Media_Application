using Social_Media_Application.Common.DTOs;
using Social_Media_Application.Common.Entities;

namespace Social_Media_Application.BusinessLogic.Interfaces
{
    public interface INotificationService
    {
        Task<List<NotiDTO>> GetNotificationsBySenderAsync(string fromUserId);
        Task<List<NotiDTO>> GetNotificationsByReceiverAsync(string toUserId);
        Task<List<NotiDTO>> GetUnreadNotificationsForUserAsync(string userId);
        Task<int> GetUnreadNotificationCountAsync(string userId);
        Task MarkAsReadAsync(int notificationId);
        Task CreateNotification(NotiUpdateDTO notification);
        Task UpdateNotification(NotiUpdateDTO notification);
        Task DeleteNotification(int notificationId);
        Task NotifyLike(string postOwnerId, string fromUserId, int postId);
        Task NotifyComment(string postOwnerId, string fromUserId, int postId);
        Task NotifyFollow(string toUserId, string fromUserId);
    }
}
