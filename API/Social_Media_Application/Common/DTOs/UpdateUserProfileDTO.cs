using Social_Media_Application.Common.Validations;

namespace Social_Media_Application.Common.DTOs
{
    public class UpdateUserProfileDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Bio { get; set; }
        public string? Location { get; set; }

        [AllowedFileExtensions(new[] { ".png", ".jpg", ".jpeg" })]
        [MaxFileSize(10 * 1024 * 1024)]
        public IFormFile? ProfilePicture { get; set; }
        public IFormFile? SecondaryProfilePicture { get; set; }

    }
}
