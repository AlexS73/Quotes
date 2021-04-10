using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quotes.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.Data
{
    public class QuotesContext: IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        public QuotesContext(DbContextOptions<QuotesContext> options): base(options)
        { }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
    }
}
