using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumiko.LinqToHtml.Interfaces
{
    using Enums;
    using System.Text.RegularExpressions;
    
    public delegate ITag ContentEvaluator(ITag parent );
    public interface ITag :IReadOnlyList<ITag>
    {
        string TagName { get; }
        string Content { get; }
        ITag ParentTag { get; }
        ContentEvaluator Evaluator { get; }
       // dynamic Evaluate();
    }
}
