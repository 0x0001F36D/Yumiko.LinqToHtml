namespace Yumiko.LinqToHtml.Tags.Infrastructure
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Interfaces;
    public class Fragment : IFragment
    {
        private static Regex at_rule;
        public IList<TagAttribute> Attributes { get; private set; }
        public string Content { get; private set; }
        public virtual Regex AttributeRule => at_rule;
        public int Count => this.Attributes.Count;

        public IEnumerable<TagAttribute> this[string key] => this.Attributes.Where(x => x.Key == key);

        public TagAttribute this[int index] => this.Attributes[index];

        public string OriginAttributes { get; private set; }

        public Fragment(string attributes, string content)
        {
            this.Content = content;
            at_rule = new Regex(@"\s(?<key>[a-zA-Z\-]+)(\s*=\s*(((?<_>['""])(?<value>[^'""]*)\k<_>)|(?<value>[^\s]+)))?", RegexOptions.ExplicitCapture);
            this.resolver(this.OriginAttributes = attributes);
        }

        private void resolver(string attributes) => 
            this.Attributes = 
            this
            .AttributeRule
            .Matches(attributes)
            .Cast<Match>()
            .Select(x => new TagAttribute(x.Groups["key"].Value, x.Groups["value"].Value)).ToList();

        public IEnumerator<TagAttribute> GetEnumerator() => this.Attributes.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public bool Contains(string key) => this.Attributes.Count(x => x.Key == key) >0;

        IEnumerator<IGrouping<string, TagAttribute>> IEnumerable<IGrouping<string, TagAttribute>>.GetEnumerator() => this.Attributes.GroupBy(x => x.Key).GetEnumerator();

        public override string ToString() => string.Join("\n", this.Attributes) + "\n" + this.Content + "\n";

    }
}
