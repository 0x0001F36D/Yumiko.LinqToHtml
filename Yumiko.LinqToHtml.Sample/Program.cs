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

    class Program
    {
        static void Main(string[] args)
        {

            var sw = new System.Diagnostics.Stopwatch();
            /*
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<book genre='novel' ISBN='1-861001-57-5'>" +
                        "<title>Pride And Prejudice</title>" +
                        "</book>");

            XmlElement rr = doc.DocumentElement;
            var attr = rr.Attributes.Cast<XmlAttribute>().Select(x => new { x.Name, Value = rr.GetAttribute(x.Name) });
            Console.ReadKey();
            return;
            ***/
            var site = new[]
            {
                "https://www.facebook.com/",
                "https://msdn.microsoft.com/zh-tw/library/system.text.regularexpressions.regex(v=vs.110).aspx",
            };

            Console.BufferHeight = 500;// nt16.MaxValue-1;
            
            var header = new WebHeaderCollection();
            header.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36");
            var html = new WebClient
            {
                Headers = header,
                Encoding = Encoding.UTF8
            }.DownloadStringTaskAsync(site[1]).Result;
            sw.Start();
            var root = Root.Create(html);
            var a = new Div(root);
            foreach (var item in a)
            {
                Console.WriteLine(item);

                Console.WriteLine("=========================================="); 
            }
            
            sw.Stop();

            Console.WriteLine(sw.Elapsed);

            Console.ReadKey();
        }

        /*
         area, base, br, col, command, embed, hr, img, input,keygen, link, meta, param, source, track, wbr
         */
    }
}