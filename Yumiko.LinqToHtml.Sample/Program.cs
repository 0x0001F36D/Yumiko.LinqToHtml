namespace Yumiko.LinqToHtml.Sample
{
    using System;
    using System.Text;
    using System.Net;
    using System.Collections.Generic;
    using Tags.Scope;
    using Yumiko.LinqToHtml.XParser;
    using System.Linq.Expressions;
    using System.Linq;

    class Program
    {
        
        static void Main(string[] args)
        {
            var sw = new System.Diagnostics.Stopwatch();

            Console.BufferHeight = short.MaxValue-1;
            
            var header = new WebHeaderCollection();
            header.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36");
            var html = new WebClient
            {
                Headers = header,
                Encoding = Encoding.UTF8
            }.DownloadStringTaskAsync("https://msdn.microsoft.com/zh-tw/library/system.text.regularexpressions.regex(v=vs.110).aspx").Result;
            sw.Start();


            var result = XParser.Load(html)
                .Query(new[]
                {
                    Scope.Custom("sentenceText",Tags.Item.TagType.Pair)
                }).Result;
            foreach (var item in result)
            {
                Console.WriteLine(item.Content);
            }

            Console.WriteLine("==============================");
            
            sw.Stop();

            Console.WriteLine(sw.Elapsed);
            Console.ReadKey();
        }
    }
}