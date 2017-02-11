using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Yumiko.LinqToHtml
{
    using Helper;
    using System.IO;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Xml;
    class Program
    {
        static void Anonymous()
        {

            var ano = new { Value = null as dynamic };
            var ano_cloner = new Func<dynamic, dynamic>((obj) => ano.GetType().GetConstructors().Single().Invoke(new[] { obj }));

            var ctor = new[] { ano }.GetType().GetConstructors()?.SingleOrDefault();
            var constructor = new Func<int, Array>((length) => (ctor?.Invoke(new[] { (dynamic)length }) as Array));
            var syncRoot = constructor(0) as object[];
            var refresh = new Func<Array>(() => syncRoot);
            var ano_list = new
            {
                SyncRoot = refresh(),
                Length = syncRoot.Length,
                Add = new Func<object, Array>((item) =>
                {
                    var length = syncRoot.Length + 1;
                    var target = constructor(length);
                    syncRoot.CopyTo(target, 0);
                    target.SetValue(item, syncRoot.Length);
                    return syncRoot = (target as object[]);
                }),
                Remove = new Func<object, Array>((object item) =>
                {
                    var target = constructor(syncRoot.Length - 1);
                    int index = 0;
                    foreach (var i in syncRoot)
                    {
                        if (i.ToString() == item.ToString()) continue;
                        target.SetValue(i, index++);
                    }
                    return syncRoot = (target as object[]);
                }),
                Clear = new Action(()=>
                {
                    Array.Clear(syncRoot, 0, syncRoot.Length);
                    Array.Resize(ref syncRoot, 0);
                }),
                IndexOf = new Func<int, object>(index => refresh().GetValue(index)),
                AsEnumerable = new Func<IEnumerable<dynamic>>(() => refresh().Cast<dynamic>())
            };

            Console.WriteLine("------");
            ano_list.Add(ano_cloner(100));
            ano_list.Add(ano_cloner(200));
            foreach (var item in ano_list.AsEnumerable())
                Console.WriteLine(item);
            
            Console.WriteLine("------");
            ano_list.Remove(ano_cloner(100));
            foreach (dynamic item in ano_list.AsEnumerable())
                Console.WriteLine(item);

            Console.WriteLine("------");
            ano_list.Add(ano_cloner(300));
            ano_list.Add(ano_cloner(400));
            foreach (var item in ano_list.AsEnumerable())
                Console.WriteLine(item);

            Console.WriteLine("------");
            ano_list.Clear();
            foreach (var item in ano_list.AsEnumerable())
                Console.WriteLine(item);
        }

        static void reg()
        {

            var html = @"<meta content=""/images/branding/googleg/1x/googleg_standard_color_128dp.png"" itemprop=""image""><p>12234566<p>5555555</meta>";
            //    var regex = new Regex(@"^(?<StartTag>(\<(?<TagName>[a-zA-Z]+)(?<Attribute> (.+?))*\>))(?<Value>(.+?))(\<[a-zA-Z ]*\/[ a-zA-Z]*\>)?$");
            var tag = "meta";

            ///@"\<{0}(?<attribute> (.+?))*\>(?<content>(.+?))(\<\/{0}\>)"
            var regex = new Regex(@"^\<" +tag+ @"(?<attribute> (.+?))*\>(?<content>(.+?))(\<\/"+tag+@"\>)?$",RegexOptions.Compiled | RegexOptions.IgnoreCase);
            foreach (var item in regex.GetGroupNames().Where(x=> !char.IsDigit( x,0)))
            {
                Console.WriteLine(item);
            }

            var o = regex.Match(html).Groups;
            Console.WriteLine("TagName :" + o["TagName"]);
            foreach (var item in o["attribute"].Captures)
            {
                Console.WriteLine("Attr : " + item);
            }
            Console.WriteLine("content : " + o["content"]);

        }
        static void Main(string[] args)
        {
            var site = new[] 
            {
                "http://lollipo.pw",
                "https://www.instagram.com/",
                "https://www.instagram.com/call_me_yu_yu/",
                "https://www.facebook.com/",
                "https://msdn.microsoft.com/zh-tw/library/system.text.regularexpressions.regex(v=vs.110).aspx",
                "http://lollipo.pw/test/nest.html"
            };

            Console.BufferHeight =500;// nt16.MaxValue-1;
            var header = new WebHeaderCollection();
            header.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36");
            var html = new WebClient
            {
                Headers = header,
                Encoding = Encoding.UTF8
            }.DownloadStringTaskAsync(site[5]).Result;
            html= new Regex(@">[\s]+<").Replace(html, "><");
            var target = "div";
            var start = new Regex($"^(<{target}(?<attribute>[^>]*)>)$");
            var end = new Regex($"^(</{target}>)$");
            var splitter = default(char);
            var stack = new Stack<int>();
            var backup = html;
            var contents = new List<string>();
            foreach (var subStr in Regex.Split(html, "(<[^>]+>)"))
            {
                #region Start Tag
                if (start.IsMatch(subStr))
                {
                    var st = html.IndexOf(subStr);
                    stack.Push(st + subStr.Length);
                    html = html.Remove(st, subStr.Length).Insert(st, new string(Enumerable.Repeat(splitter, subStr.Length).ToArray()));//new string( Enumerable.Repeat(c,len).ToArray())
                }
                #endregion
                #region EndTag
                else if (end.IsMatch(subStr))
                {
                    var st = stack.Pop();
                    var ed = html.IndexOf(subStr);
                    var content = html.Substring(st, ed - st).Replace(splitter.ToString(), null);
                    html = html
                         .Remove(st, (ed + subStr.Length) - st)
                         .Insert(st, new string(Enumerable.Repeat(splitter, (ed + subStr.Length) - st).ToArray()));
                    if (content != string.Empty)
                        contents.Add(content);
                }
                #endregion
            }
            Console.ReadKey();
            

        }

        /*
         area, base, br, col, command, embed, hr, img, input,
keygen, link, meta, param, source, track, wbr
         */
    }
}
