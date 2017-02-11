using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Yumiko.LinqToHtml.Helper
{
    public static class Extension
    {
        public static List<string> GetGroupNames(this Regex regex)=>regex.GetGroupNames().Where(x => !char.IsDigit(x, 0)).ToList();


    }
}
