
namespace Yumiko.LinqToHtml.Interfaces
{
    using System.Collections.Generic;

    public delegate IEnumerable<IFragment> FragmentHandler(string content);
    public interface ITag:IEnumerable<IFragment>
    {
        string TagName { get; }
        ITag ParentTag { get; }
        FragmentHandler GetFragments { get; }

    }
}
