namespace Yumiko.LinqToHtml.Interfaces
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Tags.Infrastructure;
    
    public interface IFragment : IReadOnlyList<TagAttribute>,ILookup<string,TagAttribute>,IEnumerable<TagAttribute>
    {
        Regex AttributeRule { get; }
        string Content { get; }
        IList<TagAttribute> Attributes { get; }
        string OriginAttributes { get; }
    }
}
