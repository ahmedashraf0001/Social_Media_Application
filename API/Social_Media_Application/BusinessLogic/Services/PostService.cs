using Microsoft.Extensions.Options;
using Social_Media_Application.BusinessLogic.Interfaces;
using Social_Media_Application.Common.DTOs;
using Social_Media_Application.Common.Entities;
using Social_Media_Application.Common.Utils.Queries;
using Social_Media_Application.DataAccess.Interfaces;
using System;

namespace Social_Media_Application.BusinessLogic.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMediaService _mediaService;
        private readonly IPostLikeRepository _postLikeRepository;
        private readonly INotificationService _notificationService;
        public PostService(
            IPostRepository postRepository,
            IMediaService mediaService,
            IPostLikeRepository postLikeRepository,
            INotificationService notificationService ) 
        { 
            _postRepository = postRepository; 
            _mediaService = mediaService;
            _postLikeRepository = postLikeRepository;
            _notificationService = notificationService;
        }
        public async Task<PostDTO> CreatePostAsync(string currentUserId, PostCreateDTO postDTO)
        {
            string url = "N/A";
            MediaType type = MediaType.None;

            if (postDTO.Media != null)
            {
                (url, type) = await _mediaService.UploadMediaAsync(postDTO.Media);
            }

            Post post = new Post()
            {
                Content = postDTO.Content,
                CreatedAt = DateTime.UtcNow,
                UserId = currentUserId,
                MediaUrl = url,
                MediaType = type
            };

            await _postRepository.AddAsync(post);

            var model = await _postRepository.GetPostAsync(post.Id, new PostQueryOptions() { IncludeAuthorDetails = true });
            PostDTO response = new PostDTO()
            {
                Id = model.Id,
                Content = model.Content,
                CreatedAt = DateTime.UtcNow,
                UserId = model.UserId,
                MediaUrl = model.MediaUrl,
                MediaType = model.MediaType,
                AuthorUsername = model.User.FirstName + " " + model.User.LastName,
                AuthorImage = model.User.PhotoUrl
            };
            return response;
        }
        public async Task DeletePostAsync(int postId)
        {
            var model = await _postRepository.GetByIdAsync(postId);
            if (model == null)
            {
                throw new InvalidOperationException(message: $"Delete failed: Post Not Found");
            }
            await _postRepository.DeleteAsync(model);
        }
        public async Task<List<PostDTO>> GenerateFeedAsync(string currentUserId, PostQueryOptions options)
        {
            var model = await _postRepository.GenerateFeedAsync(currentUserId, options);
            if (model.Count > 0) 
            {
                return model;
            }
            throw new InvalidOperationException("Your feed is empty. Start following users to see their posts!");
        }
        public async Task<PostDTO> GetPostByIdAsync(string currentUserId, int postId, PostQueryOptions options)
        {
            options.IncludeAuthorDetails = true;
            var model = await _postRepository.GetPostAsync(postId, options);
            if (model != null) 
            {
                PostDTO postDTO = new PostDTO()
                {
                    Id = postId,
                    Content = model.Content,
                    MediaUrl = model.MediaUrl,
                    MediaType = model.MediaType,
                    CreatedAt = model.CreatedAt,
                    UserId = model.UserId,
                    AuthorUsername = model.User.FirstName + " " + model.User.LastName,
                    LikeCount = model.LikesCount,
                    CommentCount = model.CommentsCount,
                    IsLikedByCurrentUser = (model.UserId == currentUserId) ? true : false,
                    AuthorImage = model.User.PhotoUrl
                };
                return postDTO;
            }
            throw new InvalidOperationException(message: "Post Not Found");
        }
        public async Task<List<PostDTO>> GetPostsByUserIdAsync(string currentUserId, string userId, PostQueryOptions options)
        {
            options.IncludeAuthorDetails = true;
            var model = await _postRepository.GetPostsByUserIdAsync(userId, options);
            List<PostDTO> posts = new List<PostDTO>();
            if (model.Count != 0)
            {
                foreach (var post in model)
                {
                    PostDTO postDTO = new PostDTO()
                    {
                        Id = post.Id,
                        Content = post.Content,
                        MediaUrl = post.MediaUrl,
                        MediaType = post.MediaType,
                        CreatedAt = post.CreatedAt,
                        UserId = post.UserId,
                        AuthorUsername = post.User.FirstName + " " + post.User.LastName,
                        AuthorImage = post.User.PhotoUrl,
                        LikeCount = post.LikesCount,
                        CommentCount = post.CommentsCount,
                        IsLikedByCurrentUser = (post.UserId == currentUserId) ? true : false,
                    };
                    posts.Add(postDTO);
                }
                return posts;
            }
            throw new InvalidOperationException(message: "Posts Not Found");
        }
        public async Task UpdatePostAsync(PostUpdateDTO postDTO)
        {
            var model = await _postRepository.GetByIdAsync(postDTO.Id);
            if (model == null)
                throw new InvalidOperationException("Post not found");

            if (postDTO.Content != null)
            {
                model.Content = postDTO.Content;
            }

            if (postDTO.Media != null)
            {
                if (!string.IsNullOrEmpty(model.MediaUrl))
                    await _mediaService.DeleteMediaAsync(model.MediaUrl);

                (var url, var type) = await _mediaService.UploadMediaAsync(postDTO.Media);
                model.MediaUrl = url;
                model.MediaType = type;
            }

            await _postRepository.UpdateAsync(model);
        }

        public async Task<bool> ToggleLikeAsync(int postId, string userId)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            if (post == null)
                throw new InvalidOperationException("Post not found");

            var existingLike = await _postLikeRepository.GetLikeAsync(postId, userId);

            if (existingLike != null)
            {
                await _postLikeRepository.RemoveLike(postId, userId);
                post.LikesCount--;
                await _postRepository.UpdateAsync(post);
                return false;
            }
            else
            {
                await _postLikeRepository.CreateLike(postId, userId);
                await _notificationService.NotifyLike(post.UserId, userId, postId);
                post.LikesCount++;
                await _postRepository.UpdateAsync(post);
                return true;
            }
            
        }
        public async Task<List<UserLikeDTO>> GetPostLikesAsync(int postId, PostQueryOptions options)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            if (post == null)
                throw new InvalidOperationException("Post not found");

            var likes = await _postLikeRepository.GetPostLikesWithUsersAsync(postId, options);

            if (likes.Count == 0)
                return new List<UserLikeDTO>();

            return likes;
        }
        public async Task<List<PostDTO>> SearchPostsAsync(string currentUserId, PostSearchQuery searchQuery, PostQueryOptions options)
        {
            options.IncludeAuthorDetails = true;
            var model = await _postRepository.SearchPostsAsync(searchQuery, options);
            List<PostDTO> posts = new List<PostDTO>();
            if (model.Count != 0)
            {
                foreach (var post in model)
                {
                    PostDTO postDTO = new PostDTO()
                    {
                        Id = post.Id,
                        Content = post.Content,
                        MediaUrl = post.MediaUrl,
                        MediaType = post.MediaType,
                        CreatedAt = post.CreatedAt,
                        UserId = post.UserId,
                        AuthorUsername = post.User.FirstName + " " + post.User.LastName,
                        AuthorImage = post.User.PhotoUrl,
                        LikeCount = post.LikesCount,
                        CommentCount = post.CommentsCount,
                        IsLikedByCurrentUser = (post.UserId == currentUserId) ? true : false,
                    };
                    posts.Add(postDTO);
                }
                return posts;
            }
            throw new InvalidOperationException(message: "Posts Not Found");
        }
    }
}
