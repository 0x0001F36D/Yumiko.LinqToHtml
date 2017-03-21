namespace Yumiko.LinqToHtml.ToolKit
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;

    public class XCrawlerFilterRuleList : List<XCrawlerFilterRule>
    {
        public bool IsMatch(IFragment fragment)
        {
           return this.All(x => !x.IsMatch(fragment)) == true;
            
        }
    }
}
