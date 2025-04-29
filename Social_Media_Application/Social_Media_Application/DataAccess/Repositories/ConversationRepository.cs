using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using Social_Media_Application.Common.DTOs;
using Social_Media_Application.Common.Entities;
using Social_Media_Application.Common.Utils.Queries;
using Social_Media_Application.DataAccess.Data;
using Social_Media_Application.DataAccess.Interfaces;

namespace Social_Media_Application.DataAccess.Repositories
{
    public class ConversationRepository : Repository<Conversation>, IConversationRepository
    {
        private readonly SocialDBContext _context;
        public ConversationRepository(SocialDBContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Conversation?> GetConversationBetweenUsersAsync(string User1Id, string User2Id, ConversationQueryOptions options)
        {
            var query = _context.conversations.AsQueryable();

            if (options.WithMessages)
            {
                query = query.Include(c => c.Messages);

                if (options.PageNumber.HasValue && options.PageSize.HasValue)
                {
                    var result = await query
                        .Where(c => c.CurrentUserId.Equals(User1Id) && c.otherUserId.Equals(User2Id))
                        .Select(c => new
                        {
                            Conversation = c,
                            PaginatedMessages = c.Messages.Skip((options.PageNumber.Value - 1) * options.PageSize.Value)
                                                           .Take(options.PageSize.Value)
                        })
                        .FirstOrDefaultAsync();

                    if (result != null)
                    {
                        result.Conversation.Messages = result.PaginatedMessages.ToList();
                        return result.Conversation;
                    }
                }
            }

            if (options.WithUserInitiated)
                query = query.Include(c => c.CurrentUser);

            if (options.WithUserReceived)
                query = query.Include(c => c.OtherUser);

            return await query.FirstOrDefaultAsync(c => c.CurrentUserId.Equals(User1Id) && c.otherUserId.Equals(User2Id));
        }
        public async Task<Conversation?> GetConversationByIdAsync(int conversationId, ConversationQueryOptions options)
        {
            var query = _context.conversations.AsQueryable();

            if (options.WithMessages)
            {
                query = query.Include(c => c.Messages);

                if (options.PageNumber.HasValue && options.PageSize.HasValue)
                {
                    var result = await query
                        .Where(c => c.Id == conversationId)
                        .Select(c => new
                        {
                            Conversation = c,
                            PaginatedMessages = c.Messages.Skip((options.PageNumber.Value - 1) * options.PageSize.Value)
                                                           .Take(options.PageSize.Value)
                        })
                        .FirstOrDefaultAsync();

                    if (result != null)
                    {
                        result.Conversation.Messages = result.PaginatedMessages.ToList();
                        return result.Conversation;
                    }
                }
            }

            if (options.WithUserInitiated)
                query = query.Include(c => c.CurrentUser);

            if (options.WithUserReceived)
                query = query.Include(c => c.OtherUser);

            return await query.FirstOrDefaultAsync(c => c.Id == conversationId);
        }

        public async Task<List<Conversation>> GetUserConversationsAsync(string userId, ConversationQueryOptions options)
        {
            var query = _context.conversations.AsQueryable();

            if (options.WithMessages)
                query = query.Include(c => c.Messages);

            if (options.WithUserInitiated)
                query = query.Include(c => c.CurrentUser);

            if (options.WithUserReceived)
                query = query.Include(c => c.OtherUser);

            query = query.Where(c => c.CurrentUserId == userId || c.otherUserId == userId);

            if (options.PageNumber.HasValue && options.PageSize.HasValue)
            {
                query = query.Skip((options.PageNumber.Value - 1) * options.PageSize.Value)
                             .Take(options.PageSize.Value);
            }

            var result = await query.ToListAsync();

            return result;
        }

        public async Task<List<Conversation>> SearchConversationsAsync(ConversationSearchQuery searchQuery, ConversationQueryOptions options)
        {
            var query = _context.conversations.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery.CurrentUserId))
            {
                query = query.Where(c => c.CurrentUserId == searchQuery.CurrentUserId);
            }

            if (!string.IsNullOrEmpty(searchQuery.OtherUserId))
            {
                query = query.Where(c => c.otherUserId == searchQuery.OtherUserId);
            }

            if (!string.IsNullOrEmpty(searchQuery.Keyword))
            {
                query = query.Where(c => c.LastMessageContent.Contains(searchQuery.Keyword) || c.ConversationName.Contains(searchQuery.Keyword));
            }

            if (searchQuery.StartDate.HasValue)
            {
                query = query.Where(c => c.CreatedAt >= searchQuery.StartDate.Value);
            }

            if (searchQuery.EndDate.HasValue)
            {
                query = query.Where(c => c.CreatedAt <= searchQuery.EndDate.Value);
            }

            if (options.WithMessages)
            {
                query = query.Include(c => c.Messages);
            }

            if (options.WithUserInitiated)
            {
                query = query.Include(c => c.CurrentUser);
            }

            if (options.WithUserReceived)
            {
                query = query.Include(c => c.OtherUser);
            }

            query = query.OrderByDescending(c => c.CreatedAt);

            if (options.PageNumber.HasValue && options.PageSize.HasValue)
            {
                query = query.Skip((options.PageNumber.Value - 1) * options.PageSize.Value)
                             .Take(options.PageSize.Value);
            }

            var result = await query.ToListAsync();

            return result;
        }
        public override async Task AddAsync(Conversation entity)
        {
            var existingConversation = await _context.conversations.FirstOrDefaultAsync(c =>
                (c.CurrentUserId == entity.CurrentUserId && c.otherUserId == entity.otherUserId) ||
                (c.CurrentUserId == entity.otherUserId && c.otherUserId == entity.CurrentUserId));

            if (existingConversation != null)
                throw new InvalidOperationException("Conversation already exists.");

            await base.AddAsync(entity);
        }
    }
}
