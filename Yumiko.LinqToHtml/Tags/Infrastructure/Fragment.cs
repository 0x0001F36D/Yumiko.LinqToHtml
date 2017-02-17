namespace Yumiko.LinqToHtml.Tags.Infrastructure
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Interfaces;
    using Helper;
    using System;

    public class Fragment : IFragment
    {
        private static Regex at_rule;
        public IList<Attribute> Attributes { get; private set; }
        public string Content { get; private set; }
        public virtual Regex AttributeRule => at_rule;
        public int Count => this.Attributes.Count;

        public IEnumerable<string> this[string key]=>this.Attributes.Where(x=>x.Key == key).Select(x=>x.Value);

        public Attribute this[int index] => this.Attributes[index];
        
        public string OriginAttributes { get; private set; }

        public Fragment(string attributes, string content)
        {
            this.Content = content;
            at_rule = new Regex(@"\s(?<key>[a-zA-Z\-]+)(\s*=\s*(((?<_>['""])(?<value>[^'""]*)\k<_>)|(?<value>[^\s]+)))?", RegexOptions.ExplicitCapture);
            this.resolver(this.OriginAttributes =attributes);
        }
        private void resolver(string attributes)
        {
            this.Attributes = this.AttributeRule.Matches(attributes).Cast<Match>().Select(x => new Attribute(x.Groups["key"].Value, x.Groups["value"].Value)).ToList();
        }

        public IEnumerator<Attribute> GetEnumerator() => this.Attributes.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public bool Contains(string key) => this.Attributes.Count(x => x.Key == key) >=1;
        IEnumerator<IGrouping<string, string>> IEnumerable<IGrouping<string, string>>.GetEnumerator()=>  this.Attributes.GroupBy(x => x.Key, x => x.Value).GetEnumerator();
        
    }
}
