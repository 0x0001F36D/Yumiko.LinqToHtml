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
            ln_rule = new Regex($"<{tagNameHandler(this.TagName)}(?<attribute>[^>]*)/>", RegexOptions.ExplicitCapture);
            this.RunFragment();
        }
        public override FragmentHandler GetFragments => getSingle;
        private ICollection<IFragment> getSingle(string html)
        {
            var l = new List<IFragment>();
            foreach (var content in Extension.HtmlSeparator(html))
            {
                if (LineTagRule.IsMatch(content))
                {
                    var v = LineTagRule.Match(content).Groups["attribute"].Value;
                    var st = html.IndexOf(content);
                    html = html.Remove(st, content.Length).Insert(st, new string(Enumerable.Repeat(EmptyCharacter, content.Length).ToArray()));
                    l.Add(new Fragment(v, null));
                }
            }
            return l;
        }


        private static Regex ln_rule;
        public virtual Regex LineTagRule => ln_rule;
    }
}
