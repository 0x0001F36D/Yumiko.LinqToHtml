using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Yumiko.LinqToHtml.Enums;

namespace Yumiko.LinqToHtml
{
    public partial class Parser : Component
    {
        public Parser()
        {
            InitializeComponent();
            this.stack = new Stack<string>();
            /*
            var bns = "Yumiko.LinqToHtml.Tags.";
            var nss = new[] { bns + typeof(Pair).Name, bns + typeof(Tags.Infrastructure.Single).Name };
            this.Tags =
                (from ns in nss
                 from t in Assembly.GetExecutingAssembly().GetTypes()
                 where t.IsClass & t.Namespace == ns & t.IsSubclassOf(typeof(Tag))
                 let ctor = t.GetConstructor(new Type[0]).Invoke(null)
                 select ctor as Tag ).ToList();
                 */
        }
        
        

        public Parser(IContainer container):this()
        { container.Add(this); }

        private Stack<string> stack;

        private string source { get; set; }
        public Parser SetSource(string source) { this.source = source; return this; }
      

    }
}
