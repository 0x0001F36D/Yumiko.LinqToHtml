

namespace Yumiko.LinqToHtml.Sample
{
    using System;
    using System.Text;
    using System.Net;
    using Yumiko.LinqToHtml.Scopes;
    using System.Collections.Generic;
    using Yumiko.LinqToHtml.ToolKit;
    using System.Linq.Expressions;
    using System.Linq;
    using Yumiko.LinqToHtml.Tags.Infrastructure;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            var sw = new System.Diagnostics.Stopwatch();

            Console.BufferHeight = short.MaxValue-1;
            var urls = new[] { "https://msdn.microsoft.com/zh-tw/library/system.text.regularexpressions.regex(v=vs.110).aspx" };

            var conf = XCrawlerConfiguration.Default(urls[0]);
            conf.Rules = new XCrawlerFilterRuleList
            {
                new XCrawlerFilterRule(FilterBy.AttributeValue , FilterMode.Capture , "msdn")
            };
            conf.Reporter = (sender, e) => Console.WriteLine(e);
  
            var invoker = new XCrawlerInvoker(conf);
            foreach (var item in invoker.Crawling().Tiers)
            {
                Console.WriteLine(item.ToString());
            } 
            
            /*
            
            var header = new WebHeaderCollection();
            header.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36");
            var html = new WebClient
            {
                Headers = header,
                Encoding  = Encoding.UTF8
            }.DownloadStringTaskAsync(urls[0]).Result;
            sw.Start();
            
            var result =  XParser.Load(html)
                .Query(new[]
                {
                    Scope.A
                });

            var query = result
                .WhenAttributeKeyHas("href")
                .SelectMany(x=>x
                    .Attributes
                  //  .Select(v=>v.Value)
                    .Where(v=>v.Value.Contains("microsoft"))
                    );

            foreach (var item in query)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("==============================");
            
            sw.Stop();

            Console.WriteLine(sw.Elapsed);*/
            Console.ReadKey();
        }
    }
}