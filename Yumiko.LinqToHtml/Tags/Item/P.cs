using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiko.LinqToHtml.Interfaces;
using Yumiko.LinqToHtml.Tags.Infrastructure;

namespace Yumiko.LinqToHtml.Tags.Item
{
    class P : PairTag
    {
        public P(ITag parent) : base(parent)
        {
        }
    }
}
