using Social_Media_Application.BusinessLogic.Interfaces;
using Social_Media_Application.Common.Entities;

namespace Social_Media_Application.BusinessLogic.Services
{
    public class MediaService:IMediaService
    {
        public async Task<(string Url, MediaType Type)> UploadMediaAsync(IFormFile? file)
        {
            if (file == null || file.Length == 0)
                return ("N/A", MediaType.None); 

            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            var extension = Path.GetExtension(file.FileName).ToLower();
            var fileName = Guid.NewGuid() + extension;
            var filePath = Path.Combine(uploadsPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var mediaUrl = $"/uploads/{fileName}";
            var mediaType = (extension == ".mp4" || extension == ".mov" || extension == ".avi") ? MediaType.Video : MediaType.Image;

            return (mediaUrl, mediaType);
        }

        public async Task<string> DeleteMediaAsync(string mediaUrl)
        {
            if (string.IsNullOrEmpty(mediaUrl))
                return "Invalid media URL.";

            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

            var fileName = mediaUrl.Replace("/uploads/", "");
            var filePath = Path.Combine(uploadsPath, fileName);

            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                    return "File deleted successfully.";
                }
                catch (Exception ex)
                {
                    return $"Error deleting file: {ex.Message}";
                }
            }

            return "File not found.";
        }
    }
}
