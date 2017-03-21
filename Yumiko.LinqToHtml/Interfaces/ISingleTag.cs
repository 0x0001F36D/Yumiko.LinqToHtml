
namespace Yumiko.LinqToHtml.Interfaces
{
    using System.Text.RegularExpressions;

    interface ISingleTag : ITag
    {
        Regex LineTagRule { get; }
    }
}
