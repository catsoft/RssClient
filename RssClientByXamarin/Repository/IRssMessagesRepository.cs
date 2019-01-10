using System.Collections.Generic;
using Database.Rss;

namespace Repository
{
    public interface IRssMessagesRepository
    {
        void MarkAsDeleted(RssMessageModel rssMessageModel);
        void MarkAsRead(RssMessageModel rssMessageModel);
        IEnumerable<RssMessageModel> GetMessagesForRss(RssModel rssModel);
        long GetCountForModel(RssModel rssModel);
    }
}
