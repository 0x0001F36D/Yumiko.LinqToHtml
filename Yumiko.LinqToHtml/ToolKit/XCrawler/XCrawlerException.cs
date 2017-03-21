using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumiko.LinqToHtml.ToolKit
{
    public class XCrawlerException : Exception
    {
        public XCrawlerException() { }
        public XCrawlerException(string message) : base(message) { }
        public XCrawlerException(string message, Exception inner) : base(message, inner) { }
    }
}
