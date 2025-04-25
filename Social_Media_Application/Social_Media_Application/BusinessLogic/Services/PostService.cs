using Social_Media_Application.BusinessLogic.Interfaces;
using Social_Media_Application.Common.DTOs;
using Social_Media_Application.Common.Entities;
using Social_Media_Application.Common.Utils;
using Social_Media_Application.DataAccess.Interfaces;

namespace Social_Media_Application.BusinessLogic.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMediaService _mediaService;
        private readonly IPostLikeRepository _postLikeRepository;
        public PostService(
            IPostRepository postRepository,
            IMediaService mediaService,
            IPostLikeRepository postLikeRepository) 
        { 
            _postRepository = postRepository; 
            _mediaService = mediaService;
            _postLikeRepository = postLikeRepository;
        }
        public async Task<Post> CreatePostAsync(PostDTO postDTO, IFormFile file)
        {
            Post post = new Post() 
            { 
                Content = postDTO.Content,
                MediaType = postDTO.MediaType,
                MediaUrl = postDTO.MediaUrl,
                CreatedAt = DateTime.UtcNow,
                UserId = postDTO.UserId,
            };
            await _postRepository.AddAsync(post);
            await _mediaService.UploadMediaAsync(file);
            return post;
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
        public async Task<List<PostDTO>> GenerateFeedAsync(string currentUserId,string userId, int pageNumber, int pageSize = 12)
        {
            var model = await _postRepository.GenerateFeedAsync(currentUserId, userId, pageNumber, pageSize);
            if (model.Count > 0) 
            {
                return model;
            }
            throw new InvalidOperationException("Your feed is empty. Start following users to see their posts!");
        }
        public async Task<PostDTO> GetPostByIdAsync(string currentUserId, int postId, PostQueryOptions options)
        {
            options.WithUsers = true;
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
                };
                return postDTO;
            }
            throw new InvalidOperationException(message: "Post Not Found");
        }
        public async Task<List<PostDTO>> GetPostsByUserIdAsync(string currentUserId, string userId, PostQueryOptions options)
        {
            options.WithUsers = true;
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
        public async Task UpdatePostAsync(int postId, PostDTO postDTO, IFormFile file)
        {
            var model = await _postRepository.GetByIdAsync(postId);
            if (model != null)
            {
                model.Id = postId;
                model.Content = postDTO.Content;
                model.MediaType = postDTO.MediaType;
                if (model.MediaUrl != postDTO.MediaUrl)
                {
                    await _mediaService.DeleteMediaAsync(model.MediaUrl);
                    await _mediaService.UploadMediaAsync(file);
                    model.MediaUrl = postDTO.MediaUrl;
                }
                await _postRepository.UpdateAsync(model);
            }
            throw new InvalidOperationException(message: "Posts Not Found");
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
                return false;
            }
            else
            {
                await _postLikeRepository.CreateLike(postId, userId);
                post.LikesCount++;
                return true;
            }
        }

    }
}
