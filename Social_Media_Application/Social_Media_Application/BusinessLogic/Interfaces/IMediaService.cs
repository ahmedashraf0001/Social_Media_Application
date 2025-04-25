namespace Social_Media_Application.BusinessLogic.Interfaces
{
    public interface IMediaService
    {
        Task<string> UploadMediaAsync(IFormFile file);
        Task<string> DeleteMediaAsync(string mediaUrl);
    }
}
