
namespace Yumiko.LinqToHtml.XCrawler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Net;
    using Tags.Infrastructure;

    public class XCrawlerConfiguration : WebHeaderCollection
    {
        public XCrawlerConfiguration(uint tier = 1, Encoding encoding = null, bool useSiteSearch = false, XCrawlerFilterRuleList rules  = null, bool useDefaultCredentials = false, IWebProxy proxy = null, ICredentials credentials = null)
        {
            this.Encoding = encoding;
            this.Proxy = proxy;
            this.Tier = tier;
            this.Rules = rules;
            this.UseSiteSearch = useSiteSearch;
            this.UseDefaultCredentials = useDefaultCredentials;
            this.Credentials = credentials;
        }

        public XCrawlerFilterRuleList Rules { get; set; }

        public bool UseSiteSearch { get; set; }

        public Encoding Encoding { get; set; }

        public IWebProxy Proxy { get; set; }

        public bool UseDefaultCredentials { get; set; }

        public ICredentials Credentials { get; set; }

        public uint Tier
        {
            get
            {
                return this._tier;
            }
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException(nameof(Tier));
                this._tier = value;

            }
        }
        private uint _tier;

        public static XCrawlerConfiguration Default => new XCrawlerConfiguration(encoding:Encoding.UTF8)
        {
            [HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36",
            [HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded;charset=utf-8"
        };
    }
}
