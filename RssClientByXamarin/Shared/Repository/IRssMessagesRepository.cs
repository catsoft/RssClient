using System.Collections.Generic;
using System.Linq;
using Shared.Database.Rss;

namespace Shared.Repository
{
    public interface IRssMessagesRepository
    {
        RssMessageModel FindById(string id);
        void MarkAsDeleted(RssMessageModel rssMessageModel);
        void MarkAsRead(RssMessageModel rssMessageModel);
        IEnumerable<RssMessageModel> GetMessagesForRss(RssModel rssModel);
        long GetCountForModel(RssModel rssModel);
        IQueryable<RssMessageModel> GetAllMessages();
    }
}
