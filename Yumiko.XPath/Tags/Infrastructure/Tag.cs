using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiko.LinqToHtml.Interfaces;

namespace Yumiko.LinqToHtml.Tags.Infrastructure
{
    public abstract class Tag :  ITag
    {

         protected List<ITag> undefinition;
        protected const char filler = default(char);

        public Tag(ITag parent)
        {
            this.TagName = this.GetType().Name;
            this.ParentTag = parent;
            this.undefinition = new List<ITag>(Evaluator?.Invoke(this));
        }
        public string Content { get; private set; }
        public abstract ContentEvaluator Evaluator { get; }
        public ITag ParentTag { get; private set; }
        public virtual string TagName { get; private set; }
        
        public int Count => ((IReadOnlyList<ITag>)this.undefinition).Count;

        public ITag this[int index]=> ((IReadOnlyList<ITag>)this.undefinition)[index];

        protected static string tagNameHandler(string tagName)=>string.Join(null, tagName.ToLower().Select(x => $"[{x}{char.ToUpper(x)}]"));

        public IEnumerator<ITag> GetEnumerator() => ((IReadOnlyList<ITag>) this.undefinition).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IReadOnlyList<ITag>)this.undefinition).GetEnumerator();
        
    }
}
