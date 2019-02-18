using System.Collections.Generic;
using Shared.Configuration.Settings;

namespace Shared.Repository.RssMessage
{
    public interface IRssMessagesRepository
    {
        RssMessageData FindById(string id);
        void MarkAsFavorite(string id);
        void MarkAsRead(string id);
        void ChangeIsFavorite(string id);
        void ChangeIsRead(string id);
        IEnumerable<RssMessageData> GetMessagesForRss(string rssId);
        long GetCountNewMessagesForModel(string rssId);
        long GetCountForModel(string rssId);
        IEnumerable<RssMessageData> GetAllMessages();
        IEnumerable<RssMessageData> GetAllFilterMessages(AllMessageFilterConfiguration filterConfiguration);
    }
}