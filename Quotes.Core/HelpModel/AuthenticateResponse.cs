using Quotes.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Quotes.Core.HelpModel
{
    public class AuthenticateResponse
    {
        public AuthenticateResponse(ApplicationUser user, string jwtToken, string refreshToken)
        {
            Id = user.Id;
            UserName = user.UserName;
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string JwtToken { get; set; }

        [JsonIgnore] //возвращается в http only cookie
        public string RefreshToken { get; set; }
    }
}
