﻿
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
    using ToolKit.Setting;
    using ToolKit.Setting.Rule;
    using System.Threading;

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
                new XCrawlerFilterRule(FilterBy.AttributeKey | FilterBy.AttributeValue | FilterBy.Content , FilterMode.Capture , string.Empty)
                /*
                new XCrawlerFilterRule(FilterBy.AttributeValue , FilterMode.Bypass , "amp"),
                new XCrawlerFilterRule(FilterBy.AttributeValue , FilterMode.Capture , "microsoft"),
                new XCrawlerFilterRule(FilterBy.AttributeValue , FilterMode.Capture , "expression"),
                new XCrawlerFilterRule(FilterBy.AttributeValue , FilterMode.Capture , "regex"),*/
            };
            conf.CallbackReporter = (sender, e) => Console.WriteLine($"[{e.Site}]");
            conf.StatusReporter = (sender, e) =>
            {
                switch (e.Status)
                {
                    case CrawlerStatus.Initializing:
                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine($"<{e.Status}>");
                        break;
                    case CrawlerStatus.Idle:
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"<{e.Status}>");
                        break;
                    case CrawlerStatus.Crawling:
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write($"<{e.Status}>");
                        break;
                    case CrawlerStatus.TargetSwitching:
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"<{e.Status}>");
                        break;
                    case CrawlerStatus.Resovling:
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.WriteLine($"<{e.Status}>");
                        break;
                    case CrawlerStatus.DataExisted:
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"<{e.Status}>");
                        break;
                    case CrawlerStatus.ReturnData:
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.Write($"<{e.Status}>");
                        break;
                }

                Console.BackgroundColor = ConsoleColor.Black;
            };

            var crawler = new XCrawler(conf);
            Console.ReadKey();
#pragma warning disable 4014
            crawler.CrawlingAsync(CancellationToken.None);
#pragma warning restore 4014
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