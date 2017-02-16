
namespace Yumiko.LinqToHtml.Tags.Item.Custom
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Interfaces;
    using Yumiko.LinqToHtml.Tags.Infrastructure;
    using System.Text.RegularExpressions;
    using Helper;

    public enum TagType
    {
        Single,
        Pair
    }
    public sealed class CustomTag : Tag, IPairTag, ISingleTag
    {
        public CustomTag(ITag parent, string tagName,TagType type , bool ignoreCase = true) : base(parent)
        {
            if (string.IsNullOrWhiteSpace(tagName))
                throw new ArgumentNullException(nameof(tagName));
            this.tagName = tagName;
            var tag = ignoreCase ? tagNameHandler(this.TagName) : this.tagName;
            switch (this.Type = type)
            {
                case TagType.Single:
                    ln_rule = new Regex($"<{tag}(?<attribute>[^>]*)/>", RegexOptions.Compiled);
                    break;
                case TagType.Pair:
                    st_rule = new Regex(@"<" + tag + "(?<attribute>[^>]*)>", RegexOptions.Compiled);
                    ed_rule = new Regex(@"</" + tag + ">", RegexOptions.Compiled);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }
            this.RunFragment();
        }
        public TagType Type { get; private set; }
        private string tagName;
        public override string TagName => tagName;
        private static Regex ed_rule;
        private static Regex st_rule;
        private static Regex ln_rule;
        public  Regex EndTagRule => ed_rule;
        public  Regex StartTagRule => st_rule;
        public Regex LineTagRule => ln_rule;
        public override FragmentHandler GetFragments => Type == TagType.Single ? (FragmentHandler)getSingle : getPair;
        private IEnumerable<IFragment> getSingle(string html)
        {
            var splitter = default(char);
            foreach (var content in Extension.HtmlSeparator(html))
            {
                if (LineTagRule.IsMatch(content))
                {
                    var v = LineTagRule.Match(content).Groups["attribute"].Value;
                    var st = html.IndexOf(content);
                    html = html.Remove(st, content.Length).Insert(st, new string(Enumerable.Repeat(splitter, content.Length).ToArray()));
                    yield return new Fragment { Attributes = v, Content = null };
                }
            }
        }
        private IEnumerable<Fragment> getPair(string html)
        {
            #region 
            var splitter = default(char);
            var stack = new Stack<Tuple<int, string>>();
            foreach (var subStr in Extension.HtmlSeparator(html))
            {
                #region Start Tag
                if (StartTagRule.IsMatch(subStr))
                {
                    var st = html.IndexOf(subStr);
                    stack.Push(Tuple.Create(st + subStr.Length, StartTagRule.Match(subStr).Groups["attribute"].Value));
                    html = html.Remove(st, subStr.Length).Insert(st, new string(Enumerable.Repeat(splitter, subStr.Length).ToArray()));//new string( Enumerable.Repeat(c,len).ToArray())
                }
                #endregion
                #region EndTag
                else if (EndTagRule.IsMatch(subStr))
                {
                    var tmp = stack.Pop();
                    var st = tmp.Item1;
                    var ed = html.IndexOf(subStr);
                    var c = html.Substring(st, ed - st).Replace(splitter.ToString(), null);
                    html = html
                         .Remove(st, (ed + subStr.Length) - st)
                         .Insert(st, new string(Enumerable.Repeat(splitter, (ed + subStr.Length) - st).ToArray()));
                    if (c != string.Empty)
                        yield return new Fragment { Content = c, Attributes = tmp.Item2 };
                }
                #endregion
            }
            #endregion
        }

    }
    
    
}
