using Quotes.Core.HelpModel;
using Quotes.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateJwtToken(ApplicationUser user);
        RefreshToken GenerateRefreshToken();
        bool RevokeToken(string token);
        AuthenticateResponse RefreshToken(string token);
    }
}
