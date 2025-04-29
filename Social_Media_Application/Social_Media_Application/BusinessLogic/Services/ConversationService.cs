using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using Social_Media_Application.BusinessLogic.Interfaces;
using Social_Media_Application.Common.DTOs;
using Social_Media_Application.Common.Entities;
using Social_Media_Application.Common.Utils.Queries;
using Social_Media_Application.DataAccess.Interfaces;

namespace Social_Media_Application.BusinessLogic.Services
{
    public class ConversationService: IConversationService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConversationRepository _conversationRepository;

        public ConversationService(IConversationRepository conversationRepository, UserManager<User> userManager) 
        {
            _conversationRepository = conversationRepository;
            _userManager = userManager;
        }

        public async Task<ConversationDTO> CreateConversationAsync(ConversationCreateDTO conversationDTO, string CurrentUserId)
        {
            var otherUser = await GetOtherUserAsync(conversationDTO.otherUserId);

            Conversation conversation = new Conversation()
            {
                CurrentUserId = CurrentUserId,
                otherUserId = conversationDTO.otherUserId,
                CreatedAt = DateTime.UtcNow,
                ConversationName = otherUser.FirstName + " " + otherUser.LastName,
                PhotoURL = otherUser.PhotoUrl
            };
            await _conversationRepository.AddAsync(conversation);
            return await MapToDTOAsync(conversation, otherUser);
        }
        
        public Task<ConversationDTO> MapToDTOAsync(Conversation conversation, User otherUser)
        {
            return Task.FromResult(new ConversationDTO()
            {
                Id = conversation.Id,
                CurrentUserId = conversation.CurrentUserId,
                OtherUserId = conversation.otherUserId,
                CreatedAt = conversation.CreatedAt,
                LastMessageAt = conversation.LastMessageAt,
                LastMessageContent = conversation.LastMessageContent,
                PhotoUrl = conversation.PhotoURL,
                ConversationName = conversation.ConversationName,
                Messages = conversation.Messages.Select(e => new MessageDTO
                {
                    Id = e.Id,
                    ConversationId = e.Id,
                    SenderId = e.SenderId,
                    ReceiverId = e.ReceiverId,
                    Content = e.Content,
                    IsRead = e.IsRead,
                    SentAt = e.SentAt
                }).ToList()
            });
        }
        public async Task DeleteConversationAsync(int conversationId)
        {
            var result = await _conversationRepository.GetConversationByIdAsync(conversationId, new ConversationQueryOptions());
            if (result == null)
            {
                throw new InvalidOperationException("No conversations found.");
            }
            await _conversationRepository.DeleteAsync(result);
        }
        
        public async Task<User> GetOtherUserAsync(string User2Id)
        {

            var otherUser = await _userManager.FindByIdAsync(User2Id);

            if (otherUser == null) { throw new InvalidOperationException(message: "User Not Found"); }

            return otherUser;
        }
        
        public async Task<ConversationDTO?> GetConversationAsync(int conversationId, string CurrentUserId, ConversationQueryOptions options)
        {
            var result = await _conversationRepository.GetConversationByIdAsync(conversationId, options);

            if (result == null)
            {
                throw new InvalidOperationException("No conversations found.");
            }
            var otherUser = await GetOtherUserAsync(result.otherUserId);

            return await MapToDTOAsync(result, otherUser);
        }

        public async Task<List<ConversationDTO>> GetUserConversationsAsync(string CurrentUserId, ConversationQueryOptions options)
        {
            var result = await _conversationRepository.GetUserConversationsAsync(CurrentUserId, options);

            if (!result.Any())
            {
                throw new InvalidOperationException("No conversations found for the specified user.");
            }

            List<ConversationDTO> model = new List<ConversationDTO>();

            foreach (var conversation in result)
            {
                var otherUser = await GetOtherUserAsync(conversation.otherUserId);

                model.Add(await MapToDTOAsync(conversation, otherUser));
            }

            return model;
        }
        public async Task<List<ConversationInboxDTO>> GetInbox(List<ConversationDTO> convo)
        {
            var model = new List<ConversationInboxDTO>();

            foreach (var conversation in convo)
            {
                var otherUser = await GetOtherUserAsync(conversation.OtherUserId);

                model.Add(new ConversationInboxDTO
                {
                    Id = conversation.Id,
                    CurrentUserId = conversation.CurrentUserId,
                    OtherUserId = conversation.OtherUserId,
                    CreatedAt = conversation.CreatedAt,
                    LastMessageAt = conversation.LastMessageAt,
                    LastMessageContent = conversation.LastMessageContent,
                    ConversationName = conversation.ConversationName,
                    PhotoUrl = conversation.PhotoUrl
                });
            }

            return model;
        }
        public async Task<ConversationDTO?> GetConversationBetweenUsersAsync(string CurrentUserId, string user2Id, ConversationQueryOptions options)
        {
            var result = await _conversationRepository.GetConversationBetweenUsersAsync(CurrentUserId, user2Id, options);

            if (result == null)
            {
                throw new InvalidOperationException("No conversations found for the specified users.");
            }

            var messagesToUpdate = result.Messages.Where(m => !m.IsRead).ToList();

            if (messagesToUpdate.Any())
            {
                foreach (var message in messagesToUpdate)
                {
                    message.IsRead = true;
                }
                await _conversationRepository.SaveChangesAsync();
            }

            var otherUser = await GetOtherUserAsync(result.otherUserId);

            return await MapToDTOAsync(result, otherUser);
        }

        public async Task<List<ConversationDTO>> SearchConversationsAsync(ConversationSearchQuery searchQuery, ConversationQueryOptions options)
        {
            var result = await _conversationRepository.SearchConversationsAsync(searchQuery, options);

            if (!result.Any())
            {
                throw new InvalidOperationException("No conversations found.");
            }
            List<ConversationDTO> model = new List<ConversationDTO>();

            foreach (var conversation in result)
            {
                var otherUser = await GetOtherUserAsync(conversation.otherUserId);

                model.Add(await MapToDTOAsync(conversation, otherUser));
            }

            return model;
        }
    }
}
