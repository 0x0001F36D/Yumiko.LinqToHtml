﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yumiko.LinqToHtml.Tags.Infrastructure;

namespace Yumiko.LinqToHtml.Interfaces
{
    interface IPairTag : ITag
    {
        Regex StartTagRule { get; }
        Regex EndTagRule { get; }
    }
}
