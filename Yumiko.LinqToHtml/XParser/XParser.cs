
namespace Yumiko.LinqToHtml.XParser
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;
    using Tags;
    using Tags.Item.Scopes;
    using Tags.Infrastructure;

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
        public XParser Query(params Scopes[] scopes)
        {
            this.Result = scopes.Aggregate(this.Result, (accumulate, aggregate) => aggregate.Generate(accumulate));
            return this;
        }
        

        public IEnumerable<Attribute> SelectAttributes(params string[] keys)=> new HashSet<Attribute>(keys.SelectMany(x => this.Result.SelectMany(xx => xx[x])));

    }
}
