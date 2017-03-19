
namespace Yumiko.LinqToHtml.XCrawler
{using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiko.LinqToHtml.Interfaces;

    public class XCrawlerFilterRuleList : List<XCrawlerFilterRule>
    {
        public bool IsMatch(IFragment fragment)
            => this.Any(x => x.IsMatch(fragment));

    }
}
