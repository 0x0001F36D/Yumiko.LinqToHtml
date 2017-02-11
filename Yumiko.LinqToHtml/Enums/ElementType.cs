using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumiko.LinqToHtml.Enums
{
    using Attributes;
    public enum ElementType
    {
        [Rule(@"\<{0}(?<attribute>(\s\w+(\=['""]?[\w \;]*?['""]?)?))*\s?\/?\>")]
        Single,
        [Rule(@"\<{0}(?<attribute>\s(.+?))*\>(?<content>(.*))(\<\/{0}\>)")]
        Pair,
        [Rule(@"(\<{0}(?<attribute>(\s\w+(\=['""]?[\w \;]*?['""]?)?))*\>)(?<content>(.*))(\<\/{0}\>)")]
        Customize
    }
}
//(?<start>\<div(?<attribute>\s(.+?))*\>)(?<content>(.*))(?<end>\<\/div\>)