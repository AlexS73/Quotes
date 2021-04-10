using Quotes.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.Interfaces
{
    public interface IUserService
    {
        ApplicationUser GetById(int id);

        Task<ClaimsIdentity> GetIdentity(ApplicationUser user);
    }
}
