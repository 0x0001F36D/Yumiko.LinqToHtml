namespace Yumiko.LinqToHtml.Tags.Item
{
    using System.Text.RegularExpressions;
    using Interfaces;
    using Yumiko.LinqToHtml.Tags.Infrastructure;

    public sealed class Doctype : SingleTag
    {
        private static Regex ln_rule;
        public Doctype(ITag parent) : base(parent)
        {
        }
        public override Regex LineTagRule => ln_rule ??( ln_rule = new Regex($"<!{tagNameHandler(this.TagName)}(?<attribute>[^>]*)>", RegexOptions.Compiled));

    }
}
