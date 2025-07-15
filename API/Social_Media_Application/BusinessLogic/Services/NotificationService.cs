using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Operators;
using Social_Media_Application.BusinessLogic.Interfaces;
using Social_Media_Application.Common.DTOs;
using Social_Media_Application.Common.Entities;
using Social_Media_Application.DataAccess.Interfaces;
using Social_Media_Application.Hubs;
using Social_Media_Application.Interfaces;
using System.Runtime.Intrinsics.Arm;
using System.Text.Json;

namespace Social_Media_Application.BusinessLogic.Services
{
    public class NotificationService: INotificationService
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly INotificationRepository notificationRepository;
        private readonly UserManager<User> userService;
        private readonly IPostRepository postrepository;
        public NotificationService(IHubContext<ChatHub> hubContext, INotificationRepository notificationRepository, UserManager<User> userService, IPostRepository postrepository)
        {
            _hubContext = hubContext;
            this.notificationRepository = notificationRepository;
            this.userService = userService;
            this.postrepository = postrepository;
        }

        public async Task NotifyLike(string postOwnerId, string fromUserId, int postId)
        {
            var user = await userService.FindByIdAsync(fromUserId);
            var msg = new NotiMsg
            {
                Message = $"{user.FirstName + " " + user.LastName} liked your post",
                UserImage = user.PhotoUrl,
                PostId = postId
            };
            await Save(postOwnerId, fromUserId, msg);
            await _hubContext.Clients.User(postOwnerId).SendAsync("ReceiveNotification", msg);
        }
        public async Task NotifyComment(string postOwnerId, string fromUserId, int postId)
        {
            var user = await userService.FindByIdAsync(fromUserId);
            var msg = new NotiMsg
            {
                Message = $"{user.FirstName + " " + user.LastName} commented on your post",
                UserImage = user.PhotoUrl,
                PostId = postId
            };
            await Save(postOwnerId, fromUserId, msg);
            await _hubContext.Clients.User(postOwnerId).SendAsync("ReceiveNotification", msg);
        }
        public async Task NotifyFollow(string toUserId, string fromUserId)
        {
            var user = await userService.FindByIdAsync(fromUserId);

            var msg = new NotiMsg
            {
                Message = $"{user.FirstName} {user.LastName} started following you",
                UserImage = user.PhotoUrl
            };

            await Save(toUserId, fromUserId, msg);
            await _hubContext.Clients.User(toUserId).SendAsync("ReceiveNotification", msg);
        }
        public async Task Save(string toUserId, string fromUserId, NotiMsg message)
        {
            var notification = new Notification
            {
                FromUserId = fromUserId,
                ToUserId = toUserId,
                Message = message.Message,
                userImage = message.UserImage,
                PostId = message.PostId,
            };

            await notificationRepository.AddAsync(notification);
            await notificationRepository.SaveChangesAsync();
        }

        public async Task<List<NotiDTO>> GetNotificationsBySenderAsync(string fromUserId)
        {
            var model = await notificationRepository.GetNotificationsBySenderAsync(fromUserId);

            var list = model
                .Select(MapToNotiDTO)
                .OrderByDescending(e => e.CreatedAt)
                .ToList();

            return list;
        }

        public NotiDTO MapToNotiDTO(Notification noti)
        {
            var model = new NotiDTO()
            {
                Id= noti.Id,
                Message= noti.Message,
                ToUserId=noti.ToUserId,
                FromUserId=noti.FromUserId, 
                CreatedAt=noti.CreatedAt,
                IsRead=noti.IsRead,
                userImage = noti.userImage,
                PostId = noti.PostId,
            };
            return model;
        }
        public async Task<List<NotiDTO>> GetNotificationsByReceiverAsync(string toUserId)
        {
            var model = await notificationRepository.GetNotificationsByReceiverAsync(toUserId);

            var list = model
                .Select(MapToNotiDTO)
                .OrderByDescending(e => e.CreatedAt)
                .ToList();

            return list;
        }

        public async Task<List<NotiDTO>> GetUnreadNotificationsForUserAsync(string userId)
        {
            var model = await notificationRepository.GetUnreadNotificationsForUserAsync(userId);

            var list = model
                .Select(MapToNotiDTO)
                .OrderByDescending(e => e.CreatedAt)
                .ToList();

            return list;
        }

        public async Task<int> GetUnreadNotificationCountAsync(string userId)
        {
            var model = await notificationRepository.GetUnreadNotificationCountAsync(userId);
            return model;
        }

        public async Task MarkAsReadAsync(int notificationId)
        {
            await notificationRepository.MarkAsReadAsync(notificationId);
        }

        public async Task CreateNotification(NotiUpdateDTO notification)
        {
            var user = await userService.FindByIdAsync(notification.FromUserId);

            Notification model = new Notification
            {
                Message = notification.Message,
                userImage = user.PhotoUrl,
                PostId = notification.PostId,
                FromUserId = notification.FromUserId,
                ToUserId = notification.ToUserId,
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            };
            await notificationRepository.AddAsync(model);
            await notificationRepository.SaveChangesAsync();    
        }

        public async Task UpdateNotification(NotiUpdateDTO notification)
        {
            var model = await notificationRepository.GetByIdAsync(notification.Id);
            model.Message = notification.Message;
            await notificationRepository.UpdateAsync(model);
            await notificationRepository.SaveChangesAsync();
        }

        public async Task DeleteNotification(int notificationId)
        {
            var model = await notificationRepository.GetByIdAsync(notificationId);
            await notificationRepository.DeleteAsync(model);
            await notificationRepository.SaveChangesAsync();
        }
    }

}
