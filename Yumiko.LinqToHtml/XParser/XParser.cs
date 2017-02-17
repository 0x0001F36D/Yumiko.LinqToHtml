using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Yumiko.LinqToHtml.Interfaces;
using Yumiko.LinqToHtml.Tags;

namespace Yumiko.LinqToHtml.XParser
{
    public sealed class XParser 
    {
        public static XParser Load(string html) => new XParser(html);
        public IList<Type> List { get; private set; }
        public string Source { get; private set; }
        private Root root;
        private XParser(string html)
        {
            this.root = Root.Create(this.Source = html);
        }



        public XParser Where()
        {
            return this;
        }

    }
}
