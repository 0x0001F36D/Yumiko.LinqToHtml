namespace Yumiko.LinqToHtml.XCrawler
{
    using XParser;

    public static class XCrawlerExtension
    {
        public static XParser AsParser(this XSiteTier tierInfo)
            => XParser.Load(tierInfo.Html);

    }
}
