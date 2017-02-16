using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yumiko.LinqToHtml.Helper;
using Yumiko.LinqToHtml.Interfaces;

namespace Yumiko.LinqToHtml.Tags.Infrastructure
{

    public abstract class PairTag : Tag, IPairTag
    {
        private static Regex ed_rule;
        private static Regex st_rule;
        public virtual Regex EndTagRule => ed_rule;
        public virtual Regex StartTagRule => st_rule;

        public PairTag(ITag parent) : base(parent)
        {
            st_rule = new Regex(@"<" + tagNameHandler(this.TagName) + "(?<attribute>[^>]*)>", RegexOptions.Compiled);
            ed_rule = new Regex(@"</" + tagNameHandler(this.TagName) + ">", RegexOptions.Compiled);
            this.RunFragment();
        }

        public override FragmentHandler GetFragments => getPair;
        private IEnumerable<Fragment> getPair(string html)
        {
            #region 
            var splitter = default(char);
            var stack = new Stack<Tuple<int, string>>();
            foreach (var subStr in Extension.HtmlSeparator(html))
            {
                #region Start Tag
                if (StartTagRule.IsMatch(subStr))
                {
                    var st = html.IndexOf(subStr);
                    stack.Push(Tuple.Create(st + subStr.Length, StartTagRule.Match(subStr).Groups["attribute"].Value));
                    html = html.Remove(st, subStr.Length).Insert(st, new string(Enumerable.Repeat(splitter, subStr.Length).ToArray()));//new string( Enumerable.Repeat(c,len).ToArray())
                }
                #endregion
                #region EndTag
                else if (EndTagRule.IsMatch(subStr))
                {
                    var tmp = stack.Pop();
                    var st = tmp.Item1;
                    var ed = html.IndexOf(subStr);
                    var c = html.Substring(st, ed - st).Replace(splitter.ToString(), null);
                    html = html
                         .Remove(st, (ed + subStr.Length) - st)
                         .Insert(st, new string(Enumerable.Repeat(splitter, (ed + subStr.Length) - st).ToArray()));
                    if (c != string.Empty)
                        yield return new Fragment { Content = c, Attributes = tmp.Item2 };
                }
                #endregion
            }
            #endregion
        }

    }
}
