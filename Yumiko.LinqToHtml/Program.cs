namespace Yumiko.LinqToHtml
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Reflection;
    using System.Net;
    using Tags;
    using System.IO;
    class Program
    {
        static void Main(string[] args)
        {
            var target = @"C:\Users\USER\Documents\Visual Studio 2015\Projects\Yumiko.LinqToHtml\Yumiko.LinqToHtml\Tags\Item\Single\";
            var tags = "area, base, br, col, command, embed, hr, img, input,keygen, link, meta, param, source, track, wbr".Replace(" ", null).Split(',').Select(x=>x[0].ToString().ToUpper()+new string(x.Skip(1).ToArray()));
            foreach (var item in tags)
            {
                using (var sw = new StreamWriter(target+$"{item}.cs"))
                {
                    var sb = new StringBuilder()

                    .AppendLine(@"namespace Yumiko.LinqToHtml.Tags.Item.Single")
                    .AppendLine("{")
                    .AppendLine("    using Interfaces;")
                    .AppendLine("    using Yumiko.LinqToHtml.Tags.Infrastructure;")
                    .AppendFormat("    class {0} : SingleTag\n", item)
                    .AppendLine("    {")
                    .Append("        public ")
                    .AppendFormat("{0}(ITag parent) : base(parent) \n", item)
                    .AppendLine("        {\n        }")
                    .AppendLine("    }")
                    .AppendLine("}");
                    sw.Write(sb.ToString());
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