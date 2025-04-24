using Social_Media_Application.Common.DTOs;

namespace Social_Media_Application.BusinessLogic.Interfaces
{
    public interface IAuthSerivce
    {
        Task RegisterUserAsync(RegisterDto registerDto);
        Task<TokenResponseDTO> LoginAsync(LoginDto loginDto);
        Task<TokenResponseDTO> CreateToken(TokenDTO tokenDTO);
    }
}
