using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yumiko.LinqToHtml.Enums;
using Yumiko.LinqToHtml.Interfaces;

namespace Yumiko.LinqToHtml.Attributes
{
    [System.AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    sealed class RuleAttribute : System.Attribute
    {
        private string rule { get; set; }
        public RuleAttribute(string rule)
        {
            this.rule = rule;
        }
        
        
    }
}
