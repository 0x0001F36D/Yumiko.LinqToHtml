namespace Yumiko.LinqToHtml.XCrawler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Net;
    using System.Collections.Specialized;
    using System.Net.NetworkInformation;
    using System.Web;
    using XParser;
    using Scope;

    public class XCrawler
    {
        protected class CrawlerClient : WebClient
        {
            public CookieContainer CookieContainer;
            public CrawlerClient()
            {
                this.CookieContainer = new CookieContainer();
            }
            protected override WebRequest GetWebRequest(Uri address)
            {
                var req = base.GetWebRequest(address);
                var hreq = req as HttpWebRequest;
                if (hreq != null)
                {
                    hreq.CookieContainer = CookieContainer;
                }
                return hreq ?? req;
            }

        }

        protected CrawlerClient XCrawlerClinet;

        public CookieContainer CookieContainer => this.XCrawlerClinet.CookieContainer;

        public XCrawlerConfiguration Config
        {
            get
            {
                return _config;
            }
            set
            {
                this._config = value;
                this.XCrawlerClinet.Headers = value as WebHeaderCollection;
                this.XCrawlerClinet.UseDefaultCredentials = value.UseDefaultCredentials;

                if (value.Encoding != null)
                    this.XCrawlerClinet.Encoding = value.Encoding;

                if (value.Credentials != null)
                    this.XCrawlerClinet.Credentials = value.Credentials;

                if (value.Proxy != null)
                    this.XCrawlerClinet.Proxy = value.Proxy;
            }
        }
        private XCrawlerConfiguration _config;
        /* Replace
        public WebHeaderCollection Headers
        {
            get
            {
                return this.XCrawlerClinet.Headers;
            }
            set
            {
                this.XCrawlerClinet.Headers = value;
            }
        }

        public Encoding Encoding
        {
            get
            {
                return this.XCrawlerClinet.Encoding;
            }
            set
            {
                this.XCrawlerClinet.Encoding = value;
            }
        }

        public IWebProxy Proxy
        {
            get
            {
                return this.XCrawlerClinet.Proxy;
            }
            set
            {
                this.XCrawlerClinet.Proxy = value;
            }
        }

        public CookieContainer CookieContainer => this.XCrawlerClinet.CookieContainer;

        public bool UseDefaultCredentials
        {
            get
            {
                return this.XCrawlerClinet.UseDefaultCredentials;
            }
            set
            {
                this.XCrawlerClinet.UseDefaultCredentials = value;
            }
        }

        public ICredentials Credentials
        {
            get
            {
                return this.XCrawlerClinet.Credentials;
            }
            set
            {
                this.XCrawlerClinet.Credentials = value;
            }
        }
        */

        public XCrawler():this(XCrawlerConfiguration.Default)
        {

        }

        public XCrawler(XCrawlerConfiguration config)
        {
            this.XCrawlerClinet = new CrawlerClient();
            this.Config = config;
        }

        private int simulateHumanPaging(uint min, uint max)
            => new Random(Guid.NewGuid().GetHashCode()).Next((int)min, (int)max);

        public IEnumerable<XSiteTier> Crawling(string url, uint delayMin = 300, uint delayMax = 3000)
        {
            var html = this.GetAsync(url).Result;
            var parser = XParser.Load(html);
            uint tier = 0;
            yield return new XSiteTier(html, tier++);

            var link = parser.Query(Scope.A).WhenAttributeKeyIs("href");

            for (; tier < _config.Tier; tier++)
            {

            }


        }


        private string convertNVC2String(NameValueCollection nvc)
            => nvc == null ? string.Empty : string.Join("&", nvc.Cast<string>().Select(p => $"{p}={HttpUtility.UrlEncode(nvc[p])}"));

        internal async Task<IEnumerable<PingReply>> PingAsync(string targetSite)
            => await Task.Run(() =>
                        from adr in Dns.GetHostEntry(targetSite).AddressList
                        let ping = new Ping()
                        let ip = adr.MapToIPv4()
                        let _ = Task.Yield()
                        select ping.SendPingAsync(ip).Result);

        internal async Task<string> PostAsync(string targetSite, NameValueCollection param)
        {
            return await this.XCrawlerClinet.UploadStringTaskAsync(targetSite, "POST", this.convertNVC2String(param));
        }

        internal async Task<string> GetAsync(string targetSite)
            => await this.XCrawlerClinet.DownloadStringTaskAsync(targetSite);

        internal async Task<string> GetAsync(string targetSite, NameValueCollection param)
            => await this.GetAsync(new StringBuilder()
                                    .Append(targetSite)
                                    .Append(param == null ? string.Empty : "?")
                                    .Append(this.convertNVC2String(param))
                                    .ToString());


    }

}
