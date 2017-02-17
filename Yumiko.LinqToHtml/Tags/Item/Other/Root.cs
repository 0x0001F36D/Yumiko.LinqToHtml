namespace Yumiko.LinqToHtml.Tags
{
    using System;
    using Interfaces;
    using Infrastructure;
    public sealed class Root : Tag
    {
        public override string TagName
        {
            get
            {
                throw new NotSupportedException();
            }
        }
        public override FragmentHandler GetFragments
        {
            get
            {
                throw new NotSupportedException();
            }
        }
        private Root(string html) : base(html) { }
        public static Root Create(string html) => new Root(html);
    }
}
