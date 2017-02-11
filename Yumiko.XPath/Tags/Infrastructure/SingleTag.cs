using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yumiko.LinqToHtml.Interfaces;

namespace Yumiko.LinqToHtml.Tags.Infrastructure
{
    public abstract class SingleTag : Tag, ISingleTag
    {
        public SingleTag(ITag parent) : base(parent)
        {
            this.LineRule = new Regex($"<{tagNameHandler(this.TagName)}[^>]/>",RegexOptions.Compiled | RegexOptions.IgnoreCase); 
        }

        public override ContentEvaluator Evaluator { get; internal set; }

        public virtual Regex LineRule { get; }
    }
}
