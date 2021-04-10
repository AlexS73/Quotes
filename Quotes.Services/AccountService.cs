using Microsoft.AspNetCore.Identity;
using Quotes.Core.HelpModel;
using Quotes.Core.Models;
using Quotes.Data;
using Quotes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.Services
{
    public class AccountService : IAccountService
    {
        private readonly QuotesContext db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITokenService tokenService;

        public AccountService(QuotesContext db, UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            this.db = db;
            this.userManager = userManager;
            this.tokenService = tokenService;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            ApplicationUser user = db.ApplicationUser.FirstOrDefault(_ => _.UserName == model.Username);

            if (user == null) return null;

            var passCorrect = await userManager.CheckPasswordAsync(user, model.Password);

            if (!passCorrect) return null;

            // генерация refresh при успешной аутентификации
            string jwtToken = await tokenService.GenerateJwtToken(user);
            RefreshToken refreshToken = tokenService.GenerateRefreshToken();

            user.RefreshTokens.Add(refreshToken);
            db.Update(user);
            await db.SaveChangesAsync();


            return new AuthenticateResponse(user, jwtToken, refreshToken.Token);
        }

        public Task<AuthenticateResponse> RefreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthenticateResponse> Registration(RegistrationRequest model)
        {
            //ApplicationUser user = db.ApplicationUser.FirstOrDefault(_ => _.UserName == model.Email);

            //if (user != null) return null;

            ApplicationUser newUser = new ApplicationUser() { UserName = model.Email, Email = model.Email };

            var res = await userManager.CreateAsync(newUser, model.Password);

            if (!res.Succeeded)
            {
                return null;
            }

            // генерация refresh при успешной аутентификации
            string jwtToken = await tokenService.GenerateJwtToken(newUser);
            RefreshToken refreshToken = tokenService.GenerateRefreshToken();

            newUser.RefreshTokens.Add(refreshToken);
            db.Update(newUser);
            await db.SaveChangesAsync();


            return new AuthenticateResponse(newUser, jwtToken, refreshToken.Token);
        }

        public bool RevokeToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
