namespace Yumiko.LinqToHtml.Tags.Item.Other
{
    using System.Text.RegularExpressions;
    using Interfaces;
    using Yumiko.LinqToHtml.Tags.Infrastructure;

    public sealed class Comment : SingleTag
    {
        private static Regex ln_rule = new Regex($"<!--(?<attribute>[^(-->)]*)-->", RegexOptions.Compiled);
        public  Comment(ITag parent) : base(parent)
        {
        }
        public override Regex LineTagRule => ln_rule;
    }
}
