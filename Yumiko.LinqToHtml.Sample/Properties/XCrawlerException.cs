
namespace Yumiko.LinqToHtml.Toolkits
{
    using System;

    public class XCrawlerException : Exception
    {
        public XCrawlerException() { }
        public XCrawlerException(string message) : base(message) { }
        public XCrawlerException(string message, Exception inner) : base(message, inner) { }
    }
}
