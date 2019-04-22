using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Shared.Configuration.Settings;

namespace Shared.Database.Rss
{
    public interface IRssMessageService
    {
        [NotNull]
        Task AddMessageAsync([CanBeNull] RssMessageServiceModel message, [CanBeNull] string idRss, CancellationToken token = default);

        [NotNull]
        [ItemCanBeNull]
        Task<RssMessageServiceModel> GetAsync([CanBeNull] string id, CancellationToken token = default);

        [NotNull]
        Task UpdateAsync([CanBeNull] RssMessageServiceModel message, CancellationToken token = default);

        [NotNull]
        [ItemNotNull]
        Task<IEnumerable<RssMessageServiceModel>> GetMessagesForRss([CanBeNull] string rssId, CancellationToken token = default);

        [NotNull]
        [ItemNotNull]
        Task<IEnumerable<RssMessageServiceModel>> GetAllMessages(CancellationToken token = default);

        [NotNull]
        [ItemNotNull]
        Task<IEnumerable<RssMessageServiceModel>> GetAllFilterMessages([CanBeNull] AllMessageFilterConfiguration filterConfiguration,
            CancellationToken token = default);

        [NotNull]
        [ItemNotNull]
        Task<IEnumerable<RssMessageServiceModel>> GetAllFavoriteMessages(CancellationToken token = default);
    }
}
