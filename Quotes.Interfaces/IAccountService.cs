using Quotes.Core.HelpModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.Interfaces
{
    public interface IAccountService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);

        Task<AuthenticateResponse> Registration(RegistrationRequest model);

        Task<AuthenticateResponse> RefreshToken(string refreshToken);
        bool RevokeToken(string token);
    }
}
