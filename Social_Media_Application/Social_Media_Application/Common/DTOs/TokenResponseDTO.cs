namespace Social_Media_Application.Common.DTOs
{
    public class TokenResponseDTO
    {
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}