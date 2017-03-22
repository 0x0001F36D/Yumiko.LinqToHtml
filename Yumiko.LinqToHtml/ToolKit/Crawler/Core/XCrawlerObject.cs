
namespace Yumiko.LinqToHtml.ToolKit.Crawler.Core
{
    using System;
    using System.Linq;

    public class XCrawlerObject
    {
        public XCrawlerObject(string site, string html, uint tier)
        {
            this.Site = site;
            this.Html = html;
            this.Tier = tier;
        }

        public string Site { get; private set; }
        public string Html { get; private set; }
        public uint Tier { get; private set; }

        public override int GetHashCode()
            => (this.Site.Sum(x => x)*this.Html.Length +
                this.Html.Sum(x => x) / this.Site.Length);

        public override bool Equals(object obj)
        {
            var o = obj as XCrawlerObject;
            return  (o.Site == this.Site) | ((o != null) ? o.GetHashCode() == this.GetHashCode() : base.Equals(obj));
        }

        public override string ToString()
            => $"#Tier-{this.Tier} [{nameof(this.Site)} : {this.Site}]\n";

    }
}
