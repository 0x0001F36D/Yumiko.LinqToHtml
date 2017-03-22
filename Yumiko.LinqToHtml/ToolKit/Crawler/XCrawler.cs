namespace Yumiko.LinqToHtml.ToolKit.Crawler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Setting;
    using Architecture;
    using System.Threading.Tasks;
    using System.Threading;
    using Core;

    public class XCrawler : IDisposable
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

        private IEnumerable<XCrawlerObject> Enumerator(XCrawlerObject site)
        {
            this.OnStatusChanged?.Invoke(this, new XCrawlerStatusEventArgs(CrawlerStatus.Resovling));
            var list = new HashSet<string>(site.Resolve(this.Config.Rules));
            foreach (var x in list)
            {
                this.OnStatusChanged?.Invoke(this, new XCrawlerStatusEventArgs(CrawlerStatus.Crawling));
                var t = new XCrawlerObject(x, new XCrawlerClient(x)
                {
                    Config = this.Config
                }.GetAsync().Result, site.Tier + 1);
                if (!this.Tiers.Contains(t))
                {
                    yield return t;
                    this.OnStatusChanged?.Invoke(this, new XCrawlerStatusEventArgs(CrawlerStatus.ReturnData));
                    this.OnCallbackInvoked?.Invoke(this, new XCrawlerCallbackEventArgs(t));
                }
                else
                    this.OnStatusChanged?.Invoke(this, new XCrawlerStatusEventArgs(CrawlerStatus.DataExisted));
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
                if (value == null)
                    this.OnStatusChanged?.Invoke(this, new XCrawlerStatusEventArgs(CrawlerStatus.Idle));
                this._isAsync = value;
            }
        }

        public void Crawling()
        {
            if (this.IsAsync == null)
                this.IsAsync = false;
            else
                throw new XCrawlerException($"Crawler is running in {(this.IsAsync.Value ? "As" : "S")}ynchonous mode now");

            var tmp = this.Tiers as IEnumerable<XCrawlerObject>;
        Flag:

            foreach (var tier in tmp)
            {
                this.OnStatusChanged(this, new XCrawlerStatusEventArgs(CrawlerStatus.TargetSwitching));
                foreach (var item in tmp = this.Enumerator(tier))
                {
                    if (!this.Tiers.Contains(item))
                        this.Tiers.Add(item);
                }

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


                var t = new Task(() =>
                {
                    var tmp = this.Tiers as IEnumerable<XCrawlerObject>;
                Flag:

                    foreach (var tier in tmp)
                    {
                        this.OnStatusChanged(this, new XCrawlerStatusEventArgs(CrawlerStatus.TargetSwitching));
                        foreach (var item in tmp = this.Enumerator(tier))
                        {
                            if (!this.Tiers.Contains(item))
                                this.Tiers.Add(item);
                        }
                        if (tier.Tier < this.Config.MaxTier)
                            goto Flag;
                    }
                }, token);
                t.Start();
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

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                    this.Tiers = null;                
                disposedValue = true;
            }
        }

        public void Dispose()
            => this.Dispose(true);

        #endregion
    }
}
