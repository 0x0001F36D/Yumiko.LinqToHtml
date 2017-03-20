namespace Yumiko.LinqToHtml.ToolKit
{
    using System.Linq;
    using System.Collections.Generic;
    using System;
    using System.Text.RegularExpressions;

    internal static class XCrawlerExtension
    {
        private static readonly Regex reg = new Regex(@"^https?:\/\/");
        internal static IEnumerable<string> Resolve(this XSiteTier tierInfo, XCrawlerFilterRuleList rules)
            => XParser.Load(tierInfo.Html)
               .Query(Scopes.Scope.A)
                .QueryResult
                .Where(rules.IsMatch)
                .SelectMany(x => x["href"].Select(v => v.Value));
            // .SelectAttributeWhenAttributeKeyIs("href")
             //  .Where(x => reg.IsMatch(x.Value))
              // .Select(x => x.Value);
    }
}
