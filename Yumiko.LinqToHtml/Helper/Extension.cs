using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yumiko.LinqToHtml.Interfaces;

namespace Yumiko.LinqToHtml.Helper
{
    public static class Extension
    {
        static readonly Regex split_rule = new Regex("(<[^>]+>)");
        public static IEnumerable<string> HtmlSeparator(string html) => split_rule.Split(html);
        

    }
}
