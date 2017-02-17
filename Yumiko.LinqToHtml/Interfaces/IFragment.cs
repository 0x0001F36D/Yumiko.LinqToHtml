using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yumiko.LinqToHtml.Tags.Infrastructure;
namespace Yumiko.LinqToHtml.Interfaces
{
    public interface IFragment : IReadOnlyList<Tags.Infrastructure.Attribute>
    {
        Regex AttributeRule { get; }
        string Content { get; }
        IList<Tags.Infrastructure.Attribute> Attributes { get; }
    }
}
