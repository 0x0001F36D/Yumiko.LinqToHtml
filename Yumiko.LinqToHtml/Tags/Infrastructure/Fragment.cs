using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiko.LinqToHtml.Interfaces;

namespace Yumiko.LinqToHtml.Tags.Infrastructure
{
    public sealed class Fragment : IFragment
    {
        public string Attributes { get; set; }
        public string Content { get; set; }
        public override string ToString() => $"Attr : {Attributes ?? string.Empty}  |  Cont : {Content?.Trim()}";

    }
}
