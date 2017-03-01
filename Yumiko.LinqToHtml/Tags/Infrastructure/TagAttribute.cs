using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumiko.LinqToHtml.Tags.Infrastructure
{
    public class TagAttribute : IEqualityComparer<TagAttribute>
    {
        public static implicit operator TagAttribute(KeyValuePair<string,string> kv)=>new TagAttribute(kv.Key,kv.Value);
        private TagAttribute()
        {

        }
        public TagAttribute(string key , string value)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            this.Key = key;
            this.Value = value ?? string.Empty;
        }
        public TagAttribute(string key) : this(key, null) { }
        public string Value { get; private set; }
        public string Key { get; private set; }
        public override string ToString() => $"[{this.Key} : {this.Value}]";

        public override bool Equals(object obj)
        {
            var o = obj as TagAttribute;
            return (o != null) ? o.ToString() == this.ToString() : false;
        }
        public override int GetHashCode()=>this.ToString().GetHashCode();

        public bool Equals(TagAttribute x, TagAttribute y)=> x.GetHashCode() == y.GetHashCode();

        public int GetHashCode(TagAttribute obj) => obj.GetHashCode();
    }
}
