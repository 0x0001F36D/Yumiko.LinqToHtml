namespace Yumiko.LinqToHtml.ToolKit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Setting;
    using Architecture;
    using System.Threading.Tasks;
    using System.Threading;

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

            this.IsAsync = null;
        }
        public event XCrawlerCallbackEventHandler OnCallbackInvoked;
        public event XCrawlerStatusEventHandler OnStatusChanged;

        public XCrawlerConfiguration Config { get; set; }
        public List<XCrawlerObject> Tiers { get; private set; }

        private IEnumerable<XCrawlerObject> Iterator(XCrawlerObject site, CrawlerStatus runInAsync)
        {

            this.OnStatusChanged?.Invoke(this, new XCrawlerStatusEventArgs(runInAsync | CrawlerStatus.Resovling | CrawlerStatus.Crawling));
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

        private bool? _isAsync;
        public bool? IsAsync
        {
            get
            {
                return _isAsync;
            }
            private set
            {
                if(value == null)
                {
                    this.OnStatusChanged?.Invoke(this, new XCrawlerStatusEventArgs(CrawlerStatus.Idle));
                }
                this._isAsync = value;
            }
        }

        public void Crawling()
        {
            if (this.IsAsync == null)
                this.IsAsync = false;
            else
                throw new XCrawlerException($"Crawler is running in {(this.IsAsync.Value ? "As" : "S")}ynchonous mode now");

            var status = (this.IsAsync.Value ? CrawlerStatus.Asynchronous : CrawlerStatus.Synchronous);
            var tmp = this.Tiers as IEnumerable<XCrawlerObject>;
        Flag:

            foreach (var tier in tmp)
            {
                this.OnStatusChanged(this, new XCrawlerStatusEventArgs(
                    status |
                    CrawlerStatus.TargetSwitching |
                    CrawlerStatus.Crawling));

                this.Tiers.AddRange(tmp = this.Iterator(tier, status));
                if (tier.Tier < this.Config.MaxTier)
                    goto Flag;
            }

            this.IsAsync = null;
        }

        public async Task CrawlingAsync(CancellationToken token)
        {
            try
            {

                if (this.IsAsync == null)
                    this.IsAsync = true;
                else
                    throw new XCrawlerException($"Crawler is running in {(this.IsAsync.Value ? "As" : "S") }ynchonous mode now");

                var status = (this.IsAsync.Value ? CrawlerStatus.Asynchronous : CrawlerStatus.Synchronous);

                var t = new Task(() =>
                {
                    var tmp = this.Tiers as IEnumerable<XCrawlerObject>;
                Flag:

                    foreach (var tier in tmp)
                    {
                        this.OnStatusChanged(this, new XCrawlerStatusEventArgs(
                            status |
                            CrawlerStatus.TargetSwitching |
                            CrawlerStatus.Crawling));

                        this.Tiers.AddRange(tmp = this.Iterator(tier, status));
                        if (tier.Tier < this.Config.MaxTier)
                            goto Flag;
                    }
                },token);

                await t;
            }
            catch (AggregateException ae)
            {
                throw new XCrawlerException("Crawler Excepted", ae.InnerExceptions.First());
            }
            finally
            {
                this.IsAsync = null;
            }
        }
    }
}
