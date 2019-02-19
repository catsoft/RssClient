using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Configuration.Settings;

namespace Shared.Repository.RssMessage
{
    public interface IRssMessagesRepository
    {
        RssMessageData FindById(string id);
        Task MarkAsFavoriteAsync(string id);
        Task MarkAsReadAsync(string id);
        Task ChangeIsFavoriteAsync(string id);
        Task ChangeIsReadAsync(string id);
        IEnumerable<RssMessageData> GetMessagesForRss(string rssId);
        long GetCountNewMessagesForModel(string rssId);
        long GetCountForModel(string rssId);
        IEnumerable<RssMessageData> GetAllMessages();
        IEnumerable<RssMessageData> GetAllFilterMessages(AllMessageFilterConfiguration filterConfiguration);
    }
}