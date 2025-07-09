using Microsoft.EntityFrameworkCore;
using Social_Media_Application.Common.Entities;
using Social_Media_Application.DataAccess.Data;
using Social_Media_Application.Interfaces;

namespace Social_Media_Application.DataAccess.Repositories
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        private readonly SocialDBContext _context;

        public NotificationRepository(SocialDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Notification>> GetNotificationsByReceiverAsync(string toUserId)
        {
            return await _context.notifications.Where(e => e.ToUserId == toUserId).ToListAsync();
        }

        public async Task<List<Notification>> GetNotificationsBySenderAsync(string fromUserId)
        {
            return await _context.notifications.Where(e => e.FromUserId == fromUserId).ToListAsync();
        }

        public async Task<int> GetUnreadNotificationCountAsync(string userId)
        {
            return await _context.notifications.Where(e =>  userId == e.ToUserId && e.IsRead == false).CountAsync();
        }

        public async Task<List<Notification>> GetUnreadNotificationsForUserAsync(string userId)
        {
            return await _context.notifications.Where(e => userId == e.ToUserId && e.IsRead == false).ToListAsync();
        }

        public async Task MarkAsReadAsync(int notificationId)
        {
            var model = await _context.notifications.FindAsync(notificationId);
            if (model != null && !model.IsRead)
            {
                model.IsRead = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
