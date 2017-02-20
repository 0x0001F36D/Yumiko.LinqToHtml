namespace Yumiko.LinqToHtml.Interfaces
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Tags.Infrastructure;
    
    public interface IFragment : IReadOnlyList<Attribute>,ILookup<string,Attribute>,IEnumerable<Attribute>
    {
        Regex AttributeRule { get; }
        string Content { get; }
        IList<Attribute> Attributes { get; }
        string OriginAttributes { get; }
    }
}
