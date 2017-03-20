namespace Yumiko.LinqToHtml.ToolKit
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;

    public class XCrawlerFilterRuleList : List<XCrawlerFilterRule>
    {
        public bool IsMatch(IFragment fragment)
            => this.Any(x => x.IsMatch(fragment));
    }
}
