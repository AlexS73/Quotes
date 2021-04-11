using Quotes.Core.HelpModel;
using Quotes.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Quotes.Services
{
    public class DataService : IDataService
    {
        public async Task<ValCurs> GetValute(DateTime date)
        {
            var request = WebRequest.CreateHttp("http://www.cbr.ru/scripts/XML_daily.asp?date_req=" + date.ToString("dd/MM/yyyy"));
            var response = await request.GetResponseAsync();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (Stream stream = response.GetResponseStream())
            {
                var serializer = new XmlSerializer(typeof(ValCurs));
                var res = serializer.Deserialize(stream);
                response.Close();
                if (res is ValCurs VC) return VC;
            }

            return null;
        }
    }
}
