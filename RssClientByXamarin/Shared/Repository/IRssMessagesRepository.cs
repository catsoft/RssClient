using System.Collections.Generic;
using Shared.Database.Rss;

namespace Shared.Repository
{
    public interface IRssMessagesRepository
    {
        void MarkAsDeleted(RssMessageModel rssMessageModel);
        void MarkAsRead(RssMessageModel rssMessageModel);
        IEnumerable<RssMessageModel> GetMessagesForRss(RssModel rssModel);
        long GetCountForModel(RssModel rssModel);
    }
}
