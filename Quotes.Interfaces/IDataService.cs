using Quotes.Core.HelpModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.Interfaces
{
    public interface IDataService
    {
        public Task<ValCurs> GetValute(DateTime date);
    }
}
