namespace Yumiko.LinqToHtml.Sample
{
    using System;
    using System.Text;
    using System.Reflection;
    using System.Net;
    using Tags;
    using System.IO;
    using System.Collections.Generic;
    using Tags.Item;
    using Tags.Item.Scopes;
    using System.Text.RegularExpressions;
    using System.Xml;
    using System.Linq;
    using System.Linq.Expressions;
    using Yumiko.LinqToHtml.XParser;
    
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.BufferHeight = short.MaxValue-1;
            var header = new WebHeaderCollection();
            header.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36");
            var html = new WebClient
            {
                Headers = header,
                Encoding = Encoding.UTF8
            }.DownloadStringTaskAsync("https://msdn.microsoft.com/zh-tw/library/system.text.regularexpressions.regex(v=vs.110).aspx").Result;


            var result = XParser
                .Load(html)
                .Query(Scopes.Div, Scopes.Span,Scopes.Img)
                .Select("src");
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("==============================");
            Console.ReadKey();
        }

        /*
         area, base, br, col, command, embed, hr, img, input,keygen, link, meta, param, source, track, wbr
         */
    }
}