using Social_Media_Application.BusinessLogic.Interfaces;
using Social_Media_Application.Common.DTOs;

namespace Social_Media_Application.BusinessLogic.Services
{
    public class AuthService : IAuthSerivce
    {
        public Task<TokenResponseDTO> CreateToken(TokenDTO tokenDTO)
        {
            throw new NotImplementedException();
        }

        public Task<TokenResponseDTO> LoginAsync(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }

        public Task RegisterUserAsync(RegisterDto registerDto)
        {
            throw new NotImplementedException();
        }
    }
}
