namespace Yumiko.LinqToHtml.Tags.Item
{
    using Helper;
    using Infrastructure;
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public enum TagType
    {
        Single,
        Pair
    }
    public sealed class Custom : Tag, IPairTag, ISingleTag
    {
        public Custom(ITag parent, string tagName,TagType type , bool ignoreCase = true) : base(parent)
        {
            if (string.IsNullOrWhiteSpace(tagName))
                throw new ArgumentNullException(nameof(tagName));
            this.tagName = tagName;
            var tag = (this.IgnoreCase =ignoreCase )? tagNameHandler(this.TagName) : this.tagName;
            switch (this.Type = type)
            {
                case TagType.Single:
                    ln_rule = new Regex($"<{tag}(?<attribute>[^>]*)/>", RegexOptions.ExplicitCapture);
                    break;
                case TagType.Pair:
                    st_rule = new Regex(@"<" + tag + "(?<attribute>[^>]*)>", RegexOptions.ExplicitCapture);
                    ed_rule = new Regex(@"</" + tag + ">");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }
            this.RunFragment();
        }
        public bool IgnoreCase { get; private set; }
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
            foreach (var content in Extension.HtmlSeparator(html))
            {
                if (LineTagRule.IsMatch(content))
                {
                    var v = LineTagRule.Match(content).Groups["attribute"].Value;
                    var st = html.IndexOf(content);
                    html = html.Remove(st, content.Length).Insert(st, new string(Enumerable.Repeat(EmptyCharacter, content.Length).ToArray()));
                    yield return new Fragment(v,null);
                }
            }
        }
        private IEnumerable<Fragment> getPair(string html)
        {
            #region 
            var stack = new Stack<Tuple<int, string>>();
            foreach (var subStr in Extension.HtmlSeparator(html))
            {
                #region Start Tag
                if (StartTagRule.IsMatch(subStr))
                {
                    var st = html.IndexOf(subStr);
                    stack.Push(Tuple.Create(st + subStr.Length, StartTagRule.Match(subStr).Groups["attribute"].Value));
                    html = html.Remove(st, subStr.Length).Insert(st, new string(Enumerable.Repeat(EmptyCharacter, subStr.Length).ToArray()));//new string( Enumerable.Repeat(c,len).ToArray())
                }
                #endregion
                #region EndTag
                else if (EndTagRule.IsMatch(subStr))
                {
                    var tmp = stack.Pop();
                    var st = tmp.Item1;
                    var ed = html.IndexOf(subStr);
                    var c = html.Substring(st, ed - st).Replace(EmptyCharacter.ToString(), null);
                    html = html
                         .Remove(st, (ed + subStr.Length) - st)
                         .Insert(st, new string(Enumerable.Repeat(EmptyCharacter, (ed + subStr.Length) - st).ToArray()));
                    if (c != string.Empty)
                        yield return new Fragment(tmp.Item2,c);
                }
                #endregion
            }
            #endregion
        }

    }
    
    
}
