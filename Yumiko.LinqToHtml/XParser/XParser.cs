
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

        public IEnumerable<IFragment> WhenContentHas(params string[] keyword)
        {
            var res = from q in this.QueryResult
                      from p in keyword
                      where q.Content.Contains(p)
                      select q;
            return  res;
        }
        public IEnumerable<IFragment> WhenAttributeKeyIs(params string[] key)
        {
            var res = from q in this.QueryResult
                      from k in key
                      where q[k].Count() >0
                      select q;
            return res;
        }

        /*
        public IEnumerable<IFragment> WhenAttributeValueHas(params string[] keyword)
        {
            var res = from q in this.QueryResult
                      from k in keyword
                      select q[k].Where(x => x.Value.Contains(k));
            return res;
        }

        public IEnumerable<TagAttribute> SelectAttributeWhenAttributeValueHas(params string[] keyword)
        {
            var res = from q in this.QueryResult
                      from k in keyword

        }*/
        public IEnumerable<TagAttribute> SelectAttributeWhenAttributeKeyIs(params string[] key)
        {
            var res = from fragment in this.WhenAttributeKeyIs(key)
                      select fragment.Attributes;
            var resw = new HashSet<TagAttribute>(res.SelectMany(x => x));
            return resw;
        }
    }
}
