using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects;

namespace Services.TokenService
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
        string GenerateToken(UserSession user);
        public string GenerateRefreshToken();
        Task<string> GenerateAndStoreRefreshToken(AppUser user);
        Task<bool> ValidateRefreshToken(AppUser user, string refreshToken);
        Task<TokenResponse> RefreshTokens(AppUser user, string refreshToken);
    }
}