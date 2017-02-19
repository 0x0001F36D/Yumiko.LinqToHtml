
namespace Yumiko.LinqToHtml.XParser
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using Yumiko.LinqToHtml.Interfaces;
    using Yumiko.LinqToHtml.Tags;
    using Yumiko.LinqToHtml.Tags.Item;
    using Yumiko.LinqToHtml.Tags.Item.Scopes;

    public sealed class XParser 
    {
        public static XParser Load(string html) => new XParser(html);
        public string Source { get; private set; }
        
        public ITag Result { get; private set; }
        private readonly Root root;
        private XParser(string html)
        {
            this.root = Root.Create(this.Source = html);
            this.OmitResult();
        }
        public XParser OmitResult()
        {
            this.Result = this.root;
            return this;
        }
        public XParser Query(params Scopes[] scopes )
        {
            this.Result = scopes.Aggregate(this.Result, (accumulate, aggregate) => aggregate.Generate(accumulate));
            return this;
        }
        public IEnumerable<string> Select(string key) => new HashSet<string>(Result?.SelectMany(x => x[key]));
        
    }
}
