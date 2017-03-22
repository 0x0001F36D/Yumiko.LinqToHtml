namespace Yumiko.LinqToHtml.ToolKit.Crawler.Setting.Rule
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;

    public class XCrawlerFilterRuleList : List<XCrawlerFilterRule>
    {
        public bool IsMatch(IFragment fragment)
            => this.All(x => !x.IsMatch(fragment)) == true;

        public readonly static XCrawlerFilterRuleList Default
            = new XCrawlerFilterRuleList { XCrawlerFilterRule.None };
    }
}
