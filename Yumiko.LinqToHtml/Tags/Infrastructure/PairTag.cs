using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yumiko.LinqToHtml.Interfaces;

namespace Yumiko.LinqToHtml.Tags.Infrastructure
{
    public abstract class PairTag : Tag, IPairTag,ITag
    {
        private static Regex st_rule;
        private static Regex ed_rule;
        public PairTag(ITag parent) : base(parent)
        {
            st_rule = new Regex(@"<" + tagNameHandler(this.TagName) + "(?<attribute>[^>]*)>");
            ed_rule = new Regex(@"</" + tagNameHandler(this.TagName) + ">");
            
        }
        public void Evaluate()
        {

        }
        private ITag runEvaluator(ITag parent,Type chil)
        {

            foreach (var content in parent.Select(x => x.Content))
            {
                var stack = new Stack<int>();
                var backup = content;
                var contents = new List<string>();
                foreach (var subStr in Regex.Split(backup, "(<[^>]+>)"))
                {
                    #region Start Tag
                    if (st_rule.IsMatch(subStr))
                    {
                        var st = backup.IndexOf(subStr);
                        stack.Push(st + subStr.Length);
                        backup = backup.Remove(st, subStr.Length).Insert(st, new string(Enumerable.Repeat(filler, subStr.Length).ToArray()));//new string( Enumerable.Repeat(c,len).ToArray())
                    }
                    #endregion
                    #region EndTag
                    else if (ed_rule.IsMatch(subStr))
                    {
                        var st = stack.Pop();
                        var ed = backup.IndexOf(subStr);
                        var c = backup.Substring(st, ed - st).Replace(filler.ToString(), null);
                        if (c != string.Empty)
                            this.undefinition.Add("/////////");

                        backup = backup
                             .Remove(st, (ed + subStr.Length) - st)
                             .Insert(st, new string(Enumerable.Repeat(filler, (ed + subStr.Length) - st).ToArray()));

                    }
                    #endregion



                } }

        }
        public override ContentEvaluator Evaluator { get; }
        public virtual Regex EndTagRule => ed_rule;
        public virtual Regex StartTagRule => st_rule;
    }
}
