using Social_Media_Application.Common.Entities;
using Social_Media_Application.DataAccess.Interfaces;


namespace Social_Media_Application.Interfaces
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Task<List<Notification>> GetNotificationsBySenderAsync(string fromUserId);
        Task<List<Notification>> GetNotificationsByReceiverAsync(string toUserId);
        Task<List<Notification>> GetUnreadNotificationsForUserAsync(string userId);
        Task<int> GetUnreadNotificationCountAsync(string userId);
        Task MarkAsReadAsync(int notificationId);
    }
}
