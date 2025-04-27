using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using Social_Media_Application.Common.DTOs;
using Social_Media_Application.Common.Entities;
using Social_Media_Application.Common.Utils;
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
                query = query.Include(c => c.Messages);

            if (options.WithUserInitiated)
                query = query.Include(c => c.User1);

            if (options.WithUserReceived)
                query = query.Include(c => c.User2);

            var result = await query.FirstOrDefaultAsync(c => c.User1Id.Equals(User1Id) && c.User2Id.Equals(User2Id));
            return result;
        }
        public async Task<Conversation?> GetConversationByIdAsync(int conversationId, ConversationQueryOptions options)
        {
            var query = _context.conversations.AsQueryable();

            if (options.WithMessages)
                query = query.Include(c => c.Messages);

            if (options.WithUserInitiated)
                query = query.Include(c => c.User1);

            if (options.WithUserReceived)
                query = query.Include(c => c.User2);

            var result = await query.FirstOrDefaultAsync(c => c.Id == conversationId);
            return result;
        }
        public async Task<List<Conversation>> GetUserConversationsAsync(string userId, ConversationQueryOptions options)
        {
            var query = _context.conversations.AsQueryable();

            if (options.WithMessages)
                query = query.Include(c => c.Messages);

            if (options.WithUserInitiated)
                query = query.Include(c => c.User1);

            if (options.WithUserReceived)
                query = query.Include(c => c.User2);

            var result = await query
                .Where(c => c.User1Id == userId || c.User2Id == userId)
                .ToListAsync();

            return result;
        }

    }
}
