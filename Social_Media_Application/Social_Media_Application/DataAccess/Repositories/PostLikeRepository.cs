﻿using Microsoft.EntityFrameworkCore;
using Social_Media_Application.Common.DTOs;
using Social_Media_Application.Common.Entities;
using Social_Media_Application.Common.Utils.Queries;
using Social_Media_Application.DataAccess.Data;
using Social_Media_Application.DataAccess.Interfaces;

namespace Social_Media_Application.DataAccess.Repositories
{
    public class PostLikeRepository : Repository<PostLike>, IPostLikeRepository
    {
        private readonly SocialDBContext _context;
        public PostLikeRepository(SocialDBContext context) : base(context)
        {
            _context = context;
        }
        public async Task<PostLike> GetLikeAsync(int postId, string userId)
        {
            return await _set.FirstOrDefaultAsync(e => e.PostId.Equals(postId) & e.UserId.Equals(userId));
        }
        public async Task CreateLike(int postId, string userId)
        {
            PostLike postLike = new PostLike()
            {
                PostId = postId,
                UserId = userId
            };

            await AddAsync(postLike);

        }
        public async Task RemoveLike(int postId, string userId)
        {
            var model = await GetLikeAsync(postId, userId);

            await DeleteAsync(model);

        }
        public async Task<List<UserLikeDTO>> GetPostLikesWithUsersAsync(int postId, PostQueryOptions options)
        {
            var query = _set
                .Where(pl => pl.PostId == postId)
                .Include(pl => pl.User)
                .Select(pl => new UserLikeDTO
                {
                    UserId = pl.UserId,
                    FullName = pl.User.FirstName + " " + pl.User.LastName,
                    PhotoUrl = pl.User.PhotoUrl
                });

            if (options.PageNumber.HasValue && options.PageSize.HasValue)
            {
                int skip = (options.PageNumber.Value - 1) * options.PageSize.Value;
                query = query.Skip(skip).Take(options.PageSize.Value);
            }

            return await query.ToListAsync();
        }

    }
}
