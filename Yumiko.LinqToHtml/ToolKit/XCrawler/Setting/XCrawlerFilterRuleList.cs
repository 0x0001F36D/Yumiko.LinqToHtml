namespace Yumiko.LinqToHtml.ToolKit.Setting.Rule
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;

    public class XCrawlerFilterRuleList : List<XCrawlerFilterRule>
    {
        public bool IsMatch(IFragment fragment)
            => this.All(x => !x.IsMatch(fragment)) == true;

    }
}
