namespace Yumiko.LinqToHtml.Sample
{
    using System;
    using System.Text;
    using System.Net;
    using System.Collections.Generic;
    using Scope;
    using Yumiko.LinqToHtml.XParser;
    using System.Linq.Expressions;
    using System.Linq;
    using Yumiko.LinqToHtml.Tags.Infrastructure;
    class Program
    {
        static void Main(string[] args)
        {
            var sw = new System.Diagnostics.Stopwatch();

            Console.BufferHeight = short.MaxValue-1;
            var urls = new[] { "https://msdn.microsoft.com/zh-tw/library/system.text.regularexpressions.regex(v=vs.110).aspx" };
            var header = new WebHeaderCollection();
            header.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36");
            var html = new WebClient
            {
                Headers = header,
                Encoding = Encoding.UTF8
            }.DownloadStringTaskAsync(urls[0]).Result;
            sw.Start();


            var result = XParser.Load(html)
                .Query(new[]
                {
                    Scope.Div.QueryByAttribute(new[] { "class" }),
                    Scope.Li,
                    Scope.A
                });
          

            foreach (var item in result.QueryResult)
            {
                
             //   var span = result.Query(Scope.Span).QueryResult;
                Console.WriteLine(item);
            }

            Console.WriteLine("==============================");
            
            sw.Stop();

            Console.WriteLine(sw.Elapsed);
            Console.ReadKey();
        }
    }
}