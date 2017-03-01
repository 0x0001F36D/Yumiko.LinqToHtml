using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yumiko.LinqToHtml.Interfaces;
using Yumiko.LinqToHtml.Tags.Infrastructure;

namespace Yumiko.LinqToHtml.Helper
{
    public static partial class Extension
    {
        static readonly Regex split_rule = new Regex("(<[^>]+>)");
        public static IEnumerable<string> HtmlSeparator(string html) => split_rule.Split(html);
        public static string OmitEmptyCharacter(this string source) => source.Replace(Tag.EmptyCharacter.ToString(), null);
    }
}
