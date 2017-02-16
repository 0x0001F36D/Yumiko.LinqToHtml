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
    public abstract class SingleTag : Tag, ISingleTag
    {
        public SingleTag(ITag parent) : base(parent)
        {
            ln_rule = new Regex($"<{tagNameHandler(this.TagName)}(?<attribute>[^>]*)/>", RegexOptions.Compiled);
            this.RunFragment();
        }
        public override FragmentHandler GetFragments => getSingle;
        private IEnumerable<IFragment> getSingle(string html)
        {
            var splitter = default(char);
            foreach (var content in Extension.HtmlSeparator(html))
            {
                if (LineTagRule.IsMatch(content))
                {
                    var v = LineTagRule.Match(content).Groups["attribute"].Value;
                    var st = html.IndexOf(content);
                    html = html.Remove(st, content.Length).Insert(st, new string(Enumerable.Repeat(splitter, content.Length).ToArray()));
                    yield return new Fragment { Attributes = v, Content = null };
                }
            }
        }


        private static Regex ln_rule;
        public virtual Regex LineTagRule => ln_rule;
    }
}
