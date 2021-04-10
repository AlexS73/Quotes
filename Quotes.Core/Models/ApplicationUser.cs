using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Quotes.Core.Models
{
    public class ApplicationUser: IdentityUser<int>
    {
        [JsonIgnore]
        public List<RefreshToken> RefreshTokens { get; set; }

    }
}
