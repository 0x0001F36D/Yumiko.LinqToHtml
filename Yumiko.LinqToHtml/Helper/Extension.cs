
namespace Yumiko.LinqToHtml.Helper
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Tags.Infrastructure;

    internal static partial class Extension
    {
        private static readonly Regex split_rule = new Regex("(<[^>]+>)");

        internal static IEnumerable<string> HtmlSeparator(string html) => split_rule.Split(html);

        internal static string OmitEmptyCharacter(this string source) => source.Replace(Tag.EmptyCharacter.ToString(), null);
    }
}
