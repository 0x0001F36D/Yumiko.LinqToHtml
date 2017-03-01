using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumiko.LinqToHtml.Interfaces
{
    using System.Text.RegularExpressions;
    using Tags.Infrastructure;

    public delegate IEnumerable<IFragment> FragmentHandler(string content);
    public interface ITag:IEnumerable<IFragment>
    {
        string TagName { get; }
        ITag ParentTag { get; }
        FragmentHandler GetFragments { get; }

    }
}
