﻿namespace Yumiko.LinqToHtml.ToolKit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Setting;
    using Architecture;
    using System.Threading.Tasks;

    public class XCrawler
    {

        public XCrawler(XCrawlerConfiguration config)
        {
            this.Config = config;
            
            if (config.StatusReporter != null)
                this.OnStatusChanged += config.StatusReporter;
            this.OnStatusChanged?.Invoke(this, new XCrawlerStatusEventArgs(CrawlerStatus.Initializing));

            if (config.CallbackReporter != null)
                this.OnCallbackInvoked += config.CallbackReporter;

            this.Tiers = new List<XCrawlerObject>(new[]
            {
                new XCrawlerObject(config.Site, new XCrawlerClient(config).GetAsync().Result, 0)
            });

            this.OnStatusChanged?.Invoke(this, new XCrawlerStatusEventArgs(CrawlerStatus.Idle));
        }
        public event XCrawlerCallbackEventHandler OnCallbackInvoked;
        public event XCrawlerStatusEventHandler OnStatusChanged;

        public XCrawlerConfiguration Config { get; set; }
        public List<XCrawlerObject> Tiers { get; private set; }

        private IEnumerable<XCrawlerObject> Iterator(XCrawlerObject site)
        {
            var list = new HashSet<string>(site.Resolve(this.Config.Rules));
            foreach (var x in list)
            {
                this.OnCallbackInvoked?.Invoke(this, new XCrawlerCallbackEventArgs(x));
                yield return new XCrawlerObject(x, new XCrawlerClient(x)
                {
                    Config = this.Config
                }.GetAsync().Result, site.Tier + 1);
            }
        }

        
        private bool? isAsync = null;

        public XCrawler Crawling()
        {
            if (this.isAsync == null)
                this.isAsync = false;
            else
                throw new XCrawlerException($"Crawler is running in {(this.isAsync.Value ? "As" : "S")}ynchonous mode now");

            var tmp = this.Tiers as IEnumerable<XCrawlerObject>;
         Flag:

            foreach (var tier in tmp)
            {
                this.OnStatusChanged(this, new XCrawlerStatusEventArgs((
                    this.isAsync.Value ? CrawlerStatus.Asynchronous : CrawlerStatus.Synchronous) |
                    CrawlerStatus.TargetSwitching |
                    CrawlerStatus.Crawling));

                this.Tiers.AddRange(tmp = this.Iterator(tier));
                if (tier.Tier < this.Config.MaxTier)
                    goto Flag;
            }

            isAsync = null;
            return this;
        }

        public Task<XCrawler> CrawlingAsync()
        {
            if (this.isAsync == null)
                this.isAsync = true;
            else
                throw new XCrawlerException($"Crawler is running in {(this.isAsync.Value ? "As" : "S") }ynchonous mode now");

            return null;
        }
    }
}
