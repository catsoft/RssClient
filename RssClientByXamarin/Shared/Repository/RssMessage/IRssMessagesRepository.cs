using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Shared.Configuration.Settings;

namespace Shared.Repository.RssMessage
{
    public interface IRssMessagesRepository
    {
        Task AddMessageAsync(RssMessageDomainModel messageDomainModel, string idRss, CancellationToken token = default);
        
        RssMessageDomainModel FindById(string id, CancellationToken token = default);

        Task MarkAsFavoriteAsync(string id, CancellationToken token = default);

        Task MarkAsReadAsync(string id, CancellationToken token = default);

        Task ChangeIsFavoriteAsync(string id, CancellationToken token = default);

        Task ChangeIsReadAsync(string id, CancellationToken token = default);

        IEnumerable<RssMessageDomainModel> GetMessagesForRss(string rssId, CancellationToken token = default);

        long GetCountNewMessagesForModel(string rssId, CancellationToken token = default);

        long GetCountForModel(string rssId, CancellationToken token = default);

        IEnumerable<RssMessageDomainModel> GetAllMessages(CancellationToken token = default);

        IEnumerable<RssMessageDomainModel> GetFavoriteMessages(CancellationToken token = default);

        IEnumerable<RssMessageDomainModel> GetAllFilterMessages(AllMessageFilterConfiguration filterConfiguration,
            CancellationToken token = default);
    }
}