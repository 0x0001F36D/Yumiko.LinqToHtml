namespace Yumiko.LinqToHtml.Interfaces
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    public interface IFragment : IReadOnlyList<Tags.Infrastructure.Attribute>,ILookup<string,string>
    {
        Regex AttributeRule { get; }
        string Content { get; }
        IList<Tags.Infrastructure.Attribute> Attributes { get; }

        string OriginAttributes { get; }
    }
}
