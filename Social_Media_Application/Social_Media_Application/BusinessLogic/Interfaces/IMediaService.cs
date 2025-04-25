using Social_Media_Application.Common.Entities;

namespace Social_Media_Application.BusinessLogic.Interfaces
{
    public interface IMediaService
    {
        Task<(string Url, MediaType Type)> UploadMediaAsync(IFormFile file);
        Task<string> DeleteMediaAsync(string mediaUrl);
    }
}
