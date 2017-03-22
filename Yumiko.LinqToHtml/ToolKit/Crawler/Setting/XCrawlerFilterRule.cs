
namespace Yumiko.LinqToHtml.ToolKit.Crawler.Setting.Rule
{
    using System;
    using System.Linq;
    using Interfaces;

    [Flags]
    public enum FilterBy
    {
        Content = 1,
        AttributeKey = 2,
        AttributeValue = 4,
    }

    public enum FilterMode
    {
        Capture,
        Bypass
    }

    public class XCrawlerFilterRule
    {
        public readonly static XCrawlerFilterRule None = new XCrawlerFilterRule(FilterBy.AttributeKey | FilterBy.AttributeValue | FilterBy.Content, FilterMode.Capture, string.Empty);

        public XCrawlerFilterRule(FilterBy filterBy, FilterMode filterMode, string match)
        {
            this.FilterBy = filterBy;
            this.FilterMode = filterMode;
            this.Match = match;
        }

        public FilterMode FilterMode { get; private set; }
        public FilterBy FilterBy { get; private set; }
        public string Match { get; private set; }

        private bool content(IFragment fragment)
        {
            var flag = fragment.Content.Contains(this.Match);
            switch (this.FilterMode)
            {
                //skip
                case FilterMode.Bypass:
                    return !flag;
                //take
                case FilterMode.Capture:
                    return flag;
            }
            return false;
        }

        private bool attributeKey(IFragment fragment)
        {
            var flag = fragment.Attributes;
            switch (this.FilterMode)
            {

                case FilterMode.Bypass:
                    return flag.Any(x => x.Key.Contains(this.Match)) == true;

                case FilterMode.Capture:
                    return flag.Any(x => x.Key.Contains(this.Match)) == false;
            }
            return false;
        }

        private bool attributeValue(IFragment fragment)
        {
            var flag = fragment.Attributes;
            switch (this.FilterMode)
            {

                case FilterMode.Bypass:
                    return flag.Any(x => x.Value.Contains(this.Match)) == true;
                
                case FilterMode.Capture:
                    return flag.Any(x => x.Value.Contains(this.Match)) == false;
            }
            return false;
        }

        public bool IsMatch(IFragment fragment)
        {

            switch (FilterBy)
            {
                case FilterBy.Content://1
                    return content(fragment);

                case FilterBy.AttributeKey://2
                    return attributeKey(fragment);

                case FilterBy.AttributeValue://4
                    return attributeValue(fragment);

                case FilterBy.AttributeKey | FilterBy.AttributeValue://6
                    return attributeKey(fragment) & attributeValue(fragment);

                case FilterBy.Content | FilterBy.AttributeValue://5
                    return content(fragment) & attributeValue(fragment);

                case FilterBy.Content | FilterBy.AttributeKey://3
                    return content(fragment) & attributeKey(fragment);

                case FilterBy.Content | FilterBy.AttributeKey | FilterBy.AttributeValue://7
                    return content(fragment) & attributeKey(fragment) & attributeValue(fragment);

                default:
                    return false;
            }
        }
    }
}
