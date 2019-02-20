using System.Collections.Generic;

namespace Shared.Api
{
    public class FeedlyRssResponce
    {
        public List<FeedlyRss> Results { get; set; }
        public string Hint { get; set; }
        public string QueryType { get; set; }
        public List<string> Related { get; set; }
        public string Scheme { get; set; }
    }
}