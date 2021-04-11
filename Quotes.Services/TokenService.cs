using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Quotes.Core.HelpModel;
using Quotes.Core.Models;
using Quotes.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.Services
{
    public class TokenService : ITokenService
    {
        private readonly IUserService userService;
        private readonly TokenModel tokenModel;

        public TokenService(IUserService userService, IOptions<TokenModel> tokenModel)
        {
            this.userService = userService;
            this.tokenModel = tokenModel.Value;
        }

        public RefreshToken GenerateRefreshToken()
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Created = DateTime.UtcNow,
                };
            }
        }

        public async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var identity = await userService.GetIdentity(user);

            var now = DateTime.UtcNow;

            byte[] key = Encoding.ASCII.GetBytes(tokenModel.Key);

            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: tokenModel.Issuer,
                    audience: tokenModel.Audience,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(tokenModel.LifeTime)),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}
