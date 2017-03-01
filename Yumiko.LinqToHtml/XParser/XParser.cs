
namespace Yumiko.LinqToHtml.XParser
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;
    using Tags;
    using Scope;
    using Tags.Infrastructure;

    public sealed class XParser
    {
        public static XParser Load(string html) => new XParser(html);
        public string Source { get; private set; }

        public ITag QueryResult { get; private set; }
        private readonly Root root;
        public IReadOnlyList<Scope> Scopes { get; private set; }
        private XParser(string html)
        {
            this.root = Root.Create(this.Source = html);
            this.OmitQueryResult();
        }
        public XParser OmitQueryResult()
        {
            this.QueryResult = this.root;
            return this;
        }
        public XParser Query(params Scope[] scopes)
        {
            this.Scopes = new List<Scope>(scopes);
            this.QueryResult = scopes.Aggregate(this.QueryResult, (accumulate, aggregate) => aggregate.Generate(accumulate));
            return this;
        }

        public IEnumerable<IFragment> WhereContent(params string[] keys)
        {
            var res = from q in this.QueryResult
                      from p in keys
                      where q.Content.Contains(p)
                      select q;
            return  res;
        }
        public IEnumerable<TagAttribute> SelectAttributes(params string[] keys) => new HashSet<TagAttribute>(keys.SelectMany(x => this.QueryResult.SelectMany(xx => xx[x])));

    }
}
