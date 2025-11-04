using JWT.Entities;
using JWT.Model;

namespace JWT.Services
{
    public interface IAuthService
    {
        Task<TokenResponseDto?> LoginAsync(UserDto request);
        Task <TokenResponseDto?>RefreshTokenAsync(RefreshTokenRequestDto request);
        Task<User?> RegisterAsync(UserDto request);
    }
}