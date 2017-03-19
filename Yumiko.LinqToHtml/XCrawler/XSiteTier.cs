using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumiko.LinqToHtml.XCrawler
{
    public class XSiteTier
    {
        public XSiteTier(string html , uint tier)
        {
            this.Html = html;
            this.Tier = tier;
        }
        public string Html { get; private set; }
        public uint Tier { get; private set; }
    }
}
