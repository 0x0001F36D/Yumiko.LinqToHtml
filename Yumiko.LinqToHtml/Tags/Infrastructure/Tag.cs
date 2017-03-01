﻿
namespace Yumiko.LinqToHtml.Tags.Infrastructure
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;
    public abstract class Tag : ITag
    {
        public const char EmptyCharacter =  default(char);
        protected List<IFragment> contents;
        public virtual string TagName { get; private set; }
        protected static string tagNameHandler(string tagName) => string.Join(null, tagName.ToLower().Select(x => $"[{x}{char.ToUpper(x)}]"));
        public IEnumerator<IFragment> GetEnumerator()=> ((IEnumerable<IFragment>)this.contents).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()=> ((IEnumerable<IFragment>)this.contents).GetEnumerator();
        public ITag ParentTag { get; private set; }

        internal Tag(string html)
        {
            if (string.IsNullOrWhiteSpace(html))
                throw new ArgumentNullException(nameof(html));
            this.contents = ((IEnumerable<IFragment>)new[] { new Fragment(string.Empty, html) }).ToList();
        }
        public Tag(ITag parent)
        {
            if (parent == null)
                throw new ArgumentNullException(nameof(parent));
            this.ParentTag = parent;
            this.TagName = this.GetType().Name;
        }
        protected void RunFragment()=> this.contents = new List<IFragment>(this.ParentTag.Select(x => GetFragments?.Invoke(x.Content)).SelectMany(x => x));
        
        public abstract FragmentHandler GetFragments { get; }
        
    }
}
