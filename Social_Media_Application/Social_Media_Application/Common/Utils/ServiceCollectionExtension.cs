using Microsoft.AspNetCore.SignalR;
using Social_Media_Application.BusinessLogic.Interfaces;
using Social_Media_Application.BusinessLogic.Services;
using Social_Media_Application.Common.Entities;
using Social_Media_Application.DataAccess.Interfaces;
using Social_Media_Application.DataAccess.Repositories;
using Social_Media_Application.Hubs;
using Social_Media_Application.Interfaces;

namespace Social_Media_Application.Common.Utils
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IPostLikeRepository, PostLikeRepository>();
            services.AddScoped<IUserFollowRepository, UserFollowRepository>();
            services.AddScoped<IConversationRepository, ConversationRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();


            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IMediaService, MediaService>();
            services.AddScoped<IConversationService, ConversationService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddSingleton<IUserIdProvider, UserIdProvider>();

            services.AddSingleton<ConnectionMapping>();

            return services;
        }
    }
}
