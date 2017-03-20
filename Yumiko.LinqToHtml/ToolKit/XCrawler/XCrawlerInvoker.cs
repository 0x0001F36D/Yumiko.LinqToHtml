

namespace Yumiko.LinqToHtml.ToolKit
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Yumiko.LinqToHtml.ToolKit;
    using Yumiko.LinqToHtml.Scopes;
    using System.Text.RegularExpressions;

    public class XCrawlerInvoker : Progress<string>
    {

        public XCrawlerInvoker(XCrawlerConfiguration config)
        {
            this.Config = config;
            if (config.Reporter != null)
                this.ProgressChanged += config.Reporter;
            this.Tiers = new List<XSiteTier>(new[] 
            {
                new XSiteTier(config.Site, new XCrawlerClient(config).GetAsync().Result, 0)
            });

        }
        

        public XCrawlerConfiguration Config { get; set; }
        public List<XSiteTier> Tiers { get; private set; }

        private IEnumerable<XSiteTier> Iterator(XSiteTier site)
        {
            var list = new HashSet<string>(site.Resolve(this.Config.Rules));
            foreach (var x in list)
            {
                this.OnReport($"Downloading : [{x}]");
             
                yield return new XSiteTier(x, new XCrawlerClient(x) { Config = this.Config }.GetAsync().Result, site.Tier + 1);
            }
        }
        public XCrawlerInvoker Crawling()
        {
            var tmp = this.Tiers as IEnumerable<XSiteTier>;
        Flag:
            this.OnReport($"Processing Tier : [{(tmp.FirstOrDefault()?.Tier.ToString() ?? "<Unknown>")}]");
            foreach (var tier in tmp)
            {
                this.Tiers.AddRange(tmp = this.Iterator(tier));
                if (tier.Tier < this.Config.MaxTier)
                    goto Flag;
            }
            return this;
        }

    }
}
