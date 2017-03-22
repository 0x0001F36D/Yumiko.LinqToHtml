
namespace Yumiko.LinqToHtml.Toolkits.Crawler.Core
{
    using System;

    public class XCrawlerException : Exception
    {
        public XCrawlerException() { }
        public XCrawlerException(string message) : base(message) { }
        public XCrawlerException(string message, Exception inner) : base(message, inner) { }
    }
}
