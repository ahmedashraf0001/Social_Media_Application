using Social_Media_Application.BusinessLogic.Interfaces;
using Social_Media_Application.BusinessLogic.Services;
using Social_Media_Application.Common.Entities;
using Social_Media_Application.DataAccess.Interfaces;
using Social_Media_Application.DataAccess.Repositories;

namespace Social_Media_Application.Common.Utils
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<Post>, PostRepository>();
            services.AddScoped<IRepository<Comment>, CommentRepository>();
            services.AddScoped<IRepository<Media>, MediaRepository>();

            services.AddScoped<IAuthSerivce, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IMediaService, MediaService>();

            return services;
        }
    }
}
