
namespace Yumiko.LinqToHtml.ToolKit
{
    using System;

    public delegate void XCrawlerCallbackEventHandler(object sender, XCrawlerCallbackEventArgs e);

    public class XCrawlerCallbackEventArgs : EventArgs
    {
        public XCrawlerCallbackEventArgs(XCrawlerObject @object)
        {
            this.Object = @object;
        }

        public XCrawlerObject Object { get; private set; }
    }
}
