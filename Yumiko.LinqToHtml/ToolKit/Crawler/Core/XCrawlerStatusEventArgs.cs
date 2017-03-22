
namespace Yumiko.LinqToHtml.ToolKit.Crawler.Core
{
    using System;
    
    public enum CrawlerStatus
    {
        Initializing = 1,
        Idle = 2,
        Crawling = 4,
        TargetSwitching = 8,
        Resovling = 0x10,
        DataExisted = 0x20,
        ReturnData = 0x40,
    }

    public delegate void XCrawlerStatusEventHandler(object sender, XCrawlerStatusEventArgs e);

    public class XCrawlerStatusEventArgs : EventArgs
    {
        public XCrawlerStatusEventArgs(CrawlerStatus status)
        {
            this.Status = status;
        }
        public CrawlerStatus Status { get; private set; }
    }
}
