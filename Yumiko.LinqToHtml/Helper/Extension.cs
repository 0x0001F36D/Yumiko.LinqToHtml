using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Yumiko.LinqToHtml.Helper
{
    public static class Extension
    {
        static readonly Regex split_rule = new Regex("(<[^>]+>)");
        public static IEnumerable<string> HtmlSeparator(string html) => split_rule.Split(html);

        public enum TagType
        {
            Single,
            Pair
        }
        public static string GenerateCSharpClassCode(string tagName , TagType type)
        {
            return new StringBuilder()
            .AppendFormat(@"namespace Yumiko.LinqToHtml.Tags.Item.{0}",type).AppendLine()
            .AppendLine("{")
            .AppendLine("    using Interfaces;")
            .AppendLine("    using Yumiko.LinqToHtml.Tags.Infrastructure;")
            .AppendFormat("    sealed class {0} : {1}Tag", tagName,type).AppendLine()
            .AppendLine("    {")
            .Append("        public ").AppendFormat("{0}(ITag parent) : base(parent)", tagName).AppendLine()
            .AppendLine("        {")
            .AppendLine("        }")
            .AppendLine("    }")
            .AppendLine("}").ToString();
        }
    }
}
