using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Shared.Configuration.Settings;

namespace Shared.Repository.RssMessage
{
    public interface IRssMessagesRepository
    {
        RssMessageData FindById(string id, CancellationToken token = default);

        Task MarkAsFavoriteAsync(string id, CancellationToken token = default);

        Task MarkAsReadAsync(string id, CancellationToken token = default);

        Task ChangeIsFavoriteAsync(string id, CancellationToken token = default);

        Task ChangeIsReadAsync(string id, CancellationToken token = default);

        IEnumerable<RssMessageData> GetMessagesForRss(string rssId, CancellationToken token = default);

        long GetCountNewMessagesForModel(string rssId, CancellationToken token = default);

        long GetCountForModel(string rssId, CancellationToken token = default);

        IEnumerable<RssMessageData> GetAllMessages(CancellationToken token = default);

        IEnumerable<RssMessageData> GetFavoriteMessages(CancellationToken token = default);

        IEnumerable<RssMessageData> GetAllFilterMessages(AllMessageFilterConfiguration filterConfiguration,
            CancellationToken token = default);
    }
}