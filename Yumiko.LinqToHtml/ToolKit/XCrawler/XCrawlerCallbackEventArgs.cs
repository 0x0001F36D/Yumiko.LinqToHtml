
namespace Yumiko.LinqToHtml.ToolKit
{
    using System;

    public delegate void XCrawlerCallbackEventHandler(object sender, XCrawlerCallbackEventArgs e);

    public class XCrawlerCallbackEventArgs : EventArgs
    {
        public XCrawlerCallbackEventArgs(string site)
        {
            this.Site = site;
        }

        public string Site { get; private set; }
    }
}
