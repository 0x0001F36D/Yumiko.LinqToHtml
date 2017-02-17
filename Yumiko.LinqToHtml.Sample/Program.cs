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

    public static class s
    {
        public static IEnumerable<string> get<Tin>(this Tin target, params Expression<Func<Tin, object>>[] selectories)
        {
            foreach (var selector in selectories)
            {
                var body = selector.Body;

                switch (body.NodeType)
                {
                    case ExpressionType.Convert:
                    yield   return ((body as UnaryExpression).Operand as MemberExpression).Member.Name;
                        break;
                    case ExpressionType.MemberAccess:
                        yield return (body as MemberExpression).Member.Name;
                        break;
                    case ExpressionType.Constant:
                        yield return (body as ConstantExpression).Value.ToString();
                        break;
                    default: yield return "";  break;
                }                
            }
            yield break;
        }
    }
    class Program
    {
        
        static void Main(string[] args)
        {
            var sw = new System.Diagnostics.Stopwatch();

            Console.BufferHeight = 500;// nt16.MaxValue-1;
            
            var header = new WebHeaderCollection();
            header.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36");
            var html = new WebClient
            {
                Headers = header,
                Encoding = Encoding.UTF8
            }.DownloadStringTaskAsync("https://msdn.microsoft.com/zh-tw/library/system.text.regularexpressions.regex(v=vs.110).aspx").Result;
            sw.Start();


            var result = XParser.XParser.Load(html)
                .Query(Scopes.Div, Scopes.Span,Scopes.Img)
                .Select("src");
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("==============================");


            /**
            var root = Root.Create(html);
            
            var itag = Scopes.Div.Generate(root);
            foreach (var item in itag)
            {
                Console.WriteLine(item.Content);
                Console.WriteLine("==============================================================");
            }
            */
            Console.ReadKey();
            return;
            sw.Stop();

            Console.WriteLine(sw.Elapsed);

            Console.ReadKey();
        }

        /*
         area, base, br, col, command, embed, hr, img, input,keygen, link, meta, param, source, track, wbr
         */
    }
}