namespace Yumiko.LinqToHtml.ToolKit.Crawler.Architecture
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Setting.Rule;
    using Core;
    using Parser;

    internal static class XCrawlerExtension
    {
        private static readonly Regex reg = new Regex(@"^https?:\/\/");
        internal static IEnumerable<string> Resolve(this XCrawlerObject tierInfo, XCrawlerFilterRuleList rules)
            => XParser.Load(tierInfo.Html)
               .Query(Scopes.Scope.A)
                .QueryResult
                .Where(rules.IsMatch)
                .SelectMany(x => 
                    x["href"]
                    .Where(v=>reg.IsMatch(v.Value))
                    .Select(v => v.Value));

        /*
        internal static TIntercept Intercept<TIntercept>(this TIntercept interceptor)
        {
            Console.WriteLine(interceptor.ToString());
            return interceptor;
        }*/
    }
}
