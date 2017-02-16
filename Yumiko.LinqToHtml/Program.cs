namespace Yumiko.LinqToHtml
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Reflection;
    using System.Net;
    using Tags;
    using System.IO;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<string> tmp;
            using(var sr = new StreamReader(@"C:\Users\USER\Documents\Visual Studio 2015\Projects\Yumiko.LinqToHtml\Yumiko.LinqToHtml\Helper\TextFile1.txt"))
            {
                tmp= sr.ReadToEnd().Split(new[] { '\n', '\r' },StringSplitOptions.RemoveEmptyEntries).Select(x=>x.Trim('<','>')).Select(x => x[0].ToString().ToUpper() + new string(x.Skip(1).ToArray()));
            }
            foreach (var item in tmp)
            {
                Console.WriteLine(item); 
            }
            Console.WriteLine("===============");
            var target = @"C:\Users\USER\Documents\Visual Studio 2015\Projects\Yumiko.LinqToHtml\Yumiko.LinqToHtml\Tags\Item\Pair\";
            Console.WriteLine(target);
            var tags = "area, base, br, col, command, embed, hr, img, input,keygen, link, meta, param, source, track, wbr".Replace(" ", null).Split(',').Select(x=>x[0].ToString().ToUpper()+new string(x.Skip(1).ToArray()));

            var except = tmp.Except(tags);

            
            foreach (var item in new[]  { "H1","H2","H3","H4","H5","H6" })
            {
                using (var sw = new StreamWriter(target+$"{item}.cs"))
                {
                    sw.Write(Yumiko.LinqToHtml.Helper.Extension.GenerateCSharpClassCode(item,Helper.Extension.TagType.Pair));
                }
            }
            
            Console.ReadKey();
            return;




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
            }.DownloadStringTaskAsync(site[4]).Result;


            Console.ReadKey();
        }

        /*
         area, base, br, col, command, embed, hr, img, input,keygen, link, meta, param, source, track, wbr
         */
    }
}