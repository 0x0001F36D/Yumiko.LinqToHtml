using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiko.LinqToHtml.Interfaces;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using System.Collections;

namespace Yumiko.LinqToHtml.Tags.Infrastructure
{
    public class Fragment : IFragment
    {
        private static Regex at_rule;
        public IList<Attribute> Attributes { get; private set; }
        public string Content { get;private set; }
        public virtual Regex AttributeRule => at_rule;
        public int Count => this.Attributes.Count;
        public Attribute this[int index]=>this.Attributes[index];
        public override string ToString() => Content;
        public Fragment(string attributes , string content)
        {
            this.Content = content;
            at_rule = new Regex(@"\s(?<key>[a-zA-Z\-]+)(\s*=\s*(((?<_>['""])(?<value>[^'""]*)\k<_>)|(?<value>[^\s]+)))?", RegexOptions.ExplicitCapture);
            this.resolver(attributes);
        }
        private void resolver(string attributes)
        {
            this.Attributes = this.AttributeRule.Matches(attributes).Cast<Match>().Select(x => new Attribute(x.Groups["key"].Value, x.Groups["value"].Value)).ToList();
        }

        public IEnumerator<Attribute> GetEnumerator() => this.Attributes.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
        
    }
}
