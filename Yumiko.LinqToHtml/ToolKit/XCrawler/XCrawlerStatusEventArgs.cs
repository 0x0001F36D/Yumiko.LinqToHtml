
namespace Yumiko.LinqToHtml.ToolKit
{
    using System;

    [Flags]
    public enum CrawlerStatus
    {
        Initializing = 0x00,

        Idle = 0x01,
        TargetSwitching = 0x02,

        Resovling = 0xEF,
        Crawling = 0xFF,

        Asynchronous = 0xAC,
        Synchronous = 0xA0,
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
