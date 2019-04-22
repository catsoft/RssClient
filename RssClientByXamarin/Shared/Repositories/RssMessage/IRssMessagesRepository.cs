using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Shared.Configuration.Settings;

namespace Shared.Repositories.RssMessage
{
    public interface IRssMessagesRepository
    {
        [NotNull]
        Task AddMessageAsync([CanBeNull] RssMessageDomainModel messageDomainModel, [CanBeNull] string idRss, CancellationToken token = default);

        [NotNull]
        [ItemCanBeNull]
        Task<RssMessageDomainModel> GetAsync([CanBeNull] string id, CancellationToken token = default);

        [NotNull]
        Task UpdateAsync([CanBeNull] RssMessageDomainModel message, CancellationToken token = default);

        [NotNull]
        [ItemNotNull]
        Task<IEnumerable<RssMessageDomainModel>> GetMessagesForRss([CanBeNull] string rssId, CancellationToken token = default);

        [NotNull]
        [ItemNotNull]
        Task<IEnumerable<RssMessageDomainModel>> GetAllMessages(CancellationToken token = default);

        [NotNull]
        [ItemNotNull]
        Task<IEnumerable<RssMessageDomainModel>> GetAllFilterMessages([CanBeNull] AllMessageFilterConfiguration filterConfiguration,
            CancellationToken token = default);

        [NotNull]
        [ItemNotNull]
        Task<IEnumerable<RssMessageDomainModel>> GetAllFavoriteMessages(CancellationToken token = default);
    }
}
