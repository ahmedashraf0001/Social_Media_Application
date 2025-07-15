using System.ComponentModel.DataAnnotations;

namespace Social_Media_Application.Common.DTOs
{
    public class ForgotPasswordRequestDTO
    {
        [EmailAddress]
        public string Email { get; set; }
    }
}
