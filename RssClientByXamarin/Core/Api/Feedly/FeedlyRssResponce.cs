using System.Collections.Generic;

namespace Core.Api.Feedly
{
    public class FeedlyRssResponce
    {
        public List<FeedlyRssApiModel> Results { get; set; }


        public string Hint { get; set; }

        public string QueryType { get; set; }

        public List<string> Related { get; set; }

        public string Scheme { get; set; }
    }
}
