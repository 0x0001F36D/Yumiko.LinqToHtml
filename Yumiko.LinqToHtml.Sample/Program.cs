namespace Yumiko.LinqToHtml.Sample
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Reflection;
    using System.Net;
    using Tags;
    using System.IO;
    using System.Collections.Generic;
    using Tags.Item.Pair;
    using Tags.Item;
    using Tags.Item.Other;

    class Program
    {
        static void Main(string[] args)
        {
            var o = from n in Assembly.GetExecutingAssembly().GetTypes()
                    where n.GetInterfaces().Any(x => x == typeof(Interfaces.ITag)) & n.IsSubclassOf(typeof(Tags.Infrastructure.Tag)) & !n.IsAbstract
                    select n;



            var site = new[]
            {
                "http://lollipo.pw",
                "https://www.instagram.com/",
                "https://www.instagram.com/call_me_yu_yu/",
                "https://www.facebook.com/",
                "https://msdn.microsoft.com/zh-tw/library/system.text.regularexpressions.regex(v=vs.110).aspx",
                "http://lollipo.pw/test/nest.html"
            };

            Console.BufferHeight = 500;// nt16.MaxValue-1;

            var header = new WebHeaderCollection();
            header.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36");
            var html = new WebClient
            {
                Headers = header,
                Encoding = Encoding.UTF8
            }.DownloadStringTaskAsync(site[5]).Result;

            Root root = Root.Create(html);
            var a = new Doctype(root);
            foreach (var item in a)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }

        /*
         area, base, br, col, command, embed, hr, img, input,keygen, link, meta, param, source, track, wbr
         */
    }
}