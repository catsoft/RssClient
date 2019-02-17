using System.Collections.Generic;
using System.Linq;
using Shared.Configuration.Settings;
using Shared.Database.Rss;

namespace Shared.Repository
{
    public interface IRssMessagesRepository
    {
        RssMessageModel FindById(string id);
        void MarkAsFavorite(RssMessageModel rssMessageModel);
        void MarkAsRead(RssMessageModel rssMessageModel);
        void ChangeIsFavorite(RssMessageModel rssMessageModel);
        void ChangeIsRead(RssMessageModel rssMessageModel);
        IEnumerable<RssMessageModel> GetMessagesForRss(RssModel rssModel);
        long GetCountNewMessagesForModel(RssModel rssModel);
        long GetCountForModel(RssModel rssModel);
        IQueryable<RssMessageModel> GetAllMessages();
        IQueryable<RssMessageModel> GetAllFilterMessages(AllMessageFilterConfiguration filterConfiguration);
    }
}