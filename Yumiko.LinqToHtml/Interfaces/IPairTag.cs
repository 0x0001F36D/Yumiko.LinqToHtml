

namespace Yumiko.LinqToHtml.Interfaces
{
    using System.Text.RegularExpressions;

    interface IPairTag : ITag
    {
        Regex StartTagRule { get; }
        Regex EndTagRule { get; }
    }
}
