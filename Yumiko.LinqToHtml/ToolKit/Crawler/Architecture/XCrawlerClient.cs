namespace Yumiko.LinqToHtml.Toolkits.Crawler.Architecture
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
    using Setting;

    internal class XCrawlerClient
    {
        private class cwClient : WebClient
        {
            public CookieContainer CookieContainer;
            public cwClient()
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

        private cwClient XCrawlerClinet;

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

        public XCrawlerClient(string site):this(XCrawlerConfiguration.Default(site))
        {

        }

        public XCrawlerClient(XCrawlerConfiguration config)
        {
            this.XCrawlerClinet = new cwClient();
            this.Config = config;
        }

        private string convertNVC2String(NameValueCollection nvc)
            => nvc == null ? string.Empty : string.Join("&", nvc.Cast<string>().Select(p => $"{p}={HttpUtility.UrlEncode(nvc[p])}"));

        internal async Task<IEnumerable<PingReply>> PingAsync()
            => await Task.Run(() =>
                        from adr in Dns.GetHostEntry(this.Config.Site).AddressList
                        let ping = new Ping()
                        let ip = adr.MapToIPv4()
                        let _ = Task.Yield()
                        select ping.SendPingAsync(ip).Result);

        internal async Task<string> PostAsync( NameValueCollection param)
            => await this.XCrawlerClinet.UploadStringTaskAsync(this.Config.Site, "POST", this.convertNVC2String(param));
        

        internal async Task<string> GetAsync()
            => await this.XCrawlerClinet.DownloadStringTaskAsync(this.Config.Site);

        internal async Task<string> GetAsync(NameValueCollection param)
            => await this.XCrawlerClinet.DownloadStringTaskAsync(new StringBuilder()
                                    .Append(this.Config.Site)
                                    .Append(param == null ? string.Empty : "?")
                                    .Append(this.convertNVC2String(param))
                                    .ToString());


    }

}
