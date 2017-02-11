using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Yumiko.LinqToHtml.Helper
{
    static class SyntaxPreprocess
    {
        private static readonly Regex sr1 = new Regex(@"\>([\s]*)\<");

        private static readonly string splitter_token = new string(new[] { '>', (char)0xFF, '<' });
        public static IEnumerable<string> Splitter(string html) =>
            sr1
                .Replace(html, splitter_token)
                .Split((char)0xFF)
                .Select(x => x.Trim());
        




    }
}
