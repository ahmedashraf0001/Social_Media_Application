using Microsoft.AspNetCore.Identity;
using Social_Media_Application.Common.DTOs;

namespace Social_Media_Application.BusinessLogic.Interfaces
{
    public interface IAuthService
    {
        Task RegisterUserAsync(RegisterDto registerDto);
        Task<TokenResponseDTO> LoginAsync(LoginDto loginDto);
        Task<TokenResponseDTO> CreateToken(TokenDTO tokenDTO);
        Task ForgotPasswordAsync(ForgotPasswordRequestDTO model);
        Task ResetPasswordAsync(ResetPasswordRequestDTO model);
        string GeneratePasswordResetLink(string frontendResetPasswordUrlBase, string token, string userEmail);
    }
}
