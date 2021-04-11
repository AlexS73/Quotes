using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Quotes.Core.HelpModel
{
    [Serializable, XmlRoot("ValCurs")]
    public class ValCurs
    {
        [XmlAttribute]
        public string Date { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("Valute")]
        public Valute[] Valutes { get; set; }
    }
}
