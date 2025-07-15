using Microsoft.EntityFrameworkCore;
using Social_Media_Application.Common.DTOs;
using Social_Media_Application.Common.Entities;
using Social_Media_Application.Common.Utils.Queries;
using Social_Media_Application.DataAccess.Data;
using Social_Media_Application.DataAccess.Interfaces;

namespace Social_Media_Application.DataAccess.Repositories
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        private readonly SocialDBContext _context;
        public MessageRepository(SocialDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Message?> GetMessageByIdAsync(int messageId, MessageQueryOptions options)
        {
            var query = _context.messages.AsQueryable(); 

            if (options.WithSenderInfo)
                query = query.Include(m => m.Sender);

            if (options.WithReceiverInfo)
                query = query.Include(m => m.Receiver);

            return await query.FirstOrDefaultAsync(m => m.Id == messageId);
        }
        public async Task<List<Message>> GetMessagesByConversationIdAsync(int conversationId, MessageQueryOptions options)
        {
            var query = _context.conversations
                .Where(e => e.Id == conversationId)
                .Include(e => e.Messages)
                .SelectMany(e => e.Messages);

            if (options.WithSenderInfo)
                query = query.Include(m => m.Sender);

            if (options.WithReceiverInfo)
                query = query.Include(m => m.Receiver);

            if (options.PageNumber.HasValue && options.PageSize.HasValue)
            {
                query = query.Skip((options.PageNumber.Value - 1) * options.PageSize.Value)
                             .Take(options.PageSize.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<List<Message>> GetUnreadMessagesForUserAsync(string userId, MessageQueryOptions options)
        {
            var query = _context.users
                .Where(u => u.Id == userId)
                .Include(u => u.ReceivedMessages)
                .SelectMany(u => u.ReceivedMessages)
                .Where(m => m.IsRead == false);

            if (options.WithSenderInfo)
                query = query.Include(m => m.Sender);

            if (options.WithReceiverInfo)
                query = query.Include(m => m.Receiver);

            if (options.PageNumber.HasValue && options.PageSize.HasValue)
            {
                query = query.Skip((options.PageNumber.Value - 1) * options.PageSize.Value)
                             .Take(options.PageSize.Value);
            }

            return await query.ToListAsync();
        }

        public async Task MarkMessageAsReadAsync(int messageId)
        {
            var model = await _context.messages.FirstOrDefaultAsync(m => m.Id == messageId);
            model.IsRead = true;
            await UpdateAsync(model);
        }
        public async Task<List<Message>> SearchMessagesAsync(MessageSearchQuery searchQuery, MessageQueryOptions options)
        {
            var query = _context.messages.AsQueryable();

            if (searchQuery.ConversationId.HasValue)
            {
                query = query.Where(m => m.ConversationId == searchQuery.ConversationId.Value);
            }

            if (!string.IsNullOrEmpty(searchQuery.SenderId))
            {
                query = query.Where(m => m.SenderId == searchQuery.SenderId);
            }

            if (!string.IsNullOrEmpty(searchQuery.RecipientId))
            {
                query = query.Where(m => m.ReceiverId == searchQuery.RecipientId);
            }

            if (!string.IsNullOrEmpty(searchQuery.Keyword))
            {
                query = query.Where(m => m.Content.Contains(searchQuery.Keyword));
            }

            if (searchQuery.StartDate.HasValue)
            {
                query = query.Where(m => m.SentAt >= searchQuery.StartDate.Value);
            }

            if (searchQuery.EndDate.HasValue)
            {
                query = query.Where(m => m.SentAt <= searchQuery.EndDate.Value);
            }

            if (options.WithSenderInfo)
            {
                query = query.Include(m => m.Sender);
            }

            if (options.WithReceiverInfo)
            {
                query = query.Include(m => m.Receiver);
            }

            query = query.OrderByDescending(m => m.SentAt);

            if (options.PageNumber.HasValue && options.PageSize.HasValue)
            {
                query = query.Skip((options.PageNumber.Value - 1) * options.PageSize.Value)
                             .Take(options.PageSize.Value);
            }

            var result = await query.ToListAsync();
            return result;
        }



        public async Task<Message> SendMessageAsync(Message message)
        {
            await AddAsync(message);
            return message;
        }
    }
}
