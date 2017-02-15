using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumiko.LinqToHtml.Interfaces
{
    public interface IFragment
    {
        string Content { get; }
        string Attributes { get; }
    }
}
