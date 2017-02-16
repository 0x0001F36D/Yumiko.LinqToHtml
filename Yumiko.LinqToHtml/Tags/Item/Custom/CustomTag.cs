
namespace Yumiko.LinqToHtml.Tags.Item.Custom
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Interfaces;
    using Yumiko.LinqToHtml.Tags.Infrastructure;

    public sealed class CustomTag : Tag
    {
        public CustomTag(ITag parent , string tagName):base(parent)
        {
            if (string.IsNullOrWhiteSpace(tagName))
                throw new ArgumentNullException(nameof(tagName));
            this.tagName = tagName;
        }
        private string tagName;
        public override string TagName => tagName;
        public override FragmentHandler GetFragments
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
