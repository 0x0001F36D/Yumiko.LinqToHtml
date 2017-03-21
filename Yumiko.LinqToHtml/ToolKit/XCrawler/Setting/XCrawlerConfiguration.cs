namespace Yumiko.LinqToHtml.ToolKit.Setting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Net;
    using Tags.Infrastructure;
    using Rule;

    public class XCrawlerConfiguration : WebHeaderCollection
    {
        public XCrawlerConfiguration(string site, uint maxTier = 1, XCrawlerStatusEventHandler statusReporter  = null, XCrawlerCallbackEventHandler callbackReporter = null, Encoding encoding = null, bool useSiteSearch = false, XCrawlerFilterRuleList rules = null, bool useDefaultCredentials = false, IWebProxy proxy = null, ICredentials credentials = null)
        {
            this.Site = site;
            this.Encoding = encoding;
            this.CallbackReporter = callbackReporter;
            this.StatusReporter = statusReporter;
            this.Proxy = proxy;
            this.MaxTier = maxTier;
            this.Rules = rules;
            this.UseSiteSearch = useSiteSearch;
            this.UseDefaultCredentials = useDefaultCredentials;
            this.Credentials = credentials;
        }

        public string Site
        {
            get
            {
                return this._site;
            }
            set
            {
                if (value == null | value == "")
                    throw new ArgumentNullException(nameof(Site));
                if (this._site == null || this._site == "")
                    this._site = value;
            }
        }
        private string _site;

        public XCrawlerCallbackEventHandler CallbackReporter { get; set; }

        public XCrawlerStatusEventHandler StatusReporter { get;  set; }

        public XCrawlerFilterRuleList Rules { get; set; }

        public bool UseSiteSearch { get; set; }

        public Encoding Encoding { get; set; }

        public IWebProxy Proxy { get; set; }

        public bool UseDefaultCredentials { get; set; }

        public ICredentials Credentials { get; set; }

        public uint MaxTier
        {
            get
            {
                return this._maxtier;
            }
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException(nameof(MaxTier));
                this._maxtier = value;

            }
        }

        private uint _maxtier;

        public static XCrawlerConfiguration Default(string site) => new XCrawlerConfiguration(site, encoding: Encoding.UTF8)
        {
            [HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36",
            [HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded;charset=utf-8"
        };
    }
}
