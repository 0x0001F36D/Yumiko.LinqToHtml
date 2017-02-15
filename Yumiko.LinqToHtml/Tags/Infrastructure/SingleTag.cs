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
    abstract class SingleTag : Tag, ISingleTag
    {
        public SingleTag(Tag parent) : base(parent)
        {
            ln_rule = new Regex($"<{tagNameHandler(this.TagName)}(?<attribute>[^>]*)/>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            this.RunFragment();
        }
        public override FragmentHandler GetFragments => getTag;
        private IEnumerable<IFragment> getTag(string html)
        {
            foreach (var content in Extension.HtmlSeparator(html))
            {
                if (LineTagRule.IsMatch(content))
                {
                    var v = LineTagRule.Match(content).Groups["attribute"].Value;
                    yield return new Fragment { Attributes = v, Content = null };
                }
            }
        }


        private static Regex ln_rule;
        public virtual Regex LineTagRule => ln_rule;
    }
}
