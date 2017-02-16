

namespace Yumiko.LinqToHtml.Tags.Item
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Yumiko.LinqToHtml.Interfaces;
    using Yumiko.LinqToHtml.Tags.Infrastructure;
    sealed class Root : Tag
    {
        public override string TagName
        {
            get
            {
                throw new NotSupportedException();
            }
        }
        public override FragmentHandler GetFragments { get { throw new NotSupportedException(); } }
        private Root(string initHtml) : base(initHtml) { }
        public static Root Create(string html) => new Root(html);
    }
}
