using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Configuration.AllMessageFilter;
using JetBrains.Annotations;

namespace Core.Services.RssMessages
{
    public interface IRssMessageService
    {
        [NotNull]
        Task AddMessageAsync([CanBeNull] RssMessageServiceModel message, Guid idRss, CancellationToken token = default);

        [NotNull]
        [ItemCanBeNull]
        Task<RssMessageServiceModel> GetAsync(Guid id, CancellationToken token = default);

        [NotNull]
        Task UpdateAsync([CanBeNull] RssMessageServiceModel message, CancellationToken token = default);

        [NotNull]
        [ItemNotNull]
        Task<IEnumerable<RssMessageServiceModel>> GetMessagesForRss(Guid rssId, CancellationToken token = default);

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

        [NotNull]
        Task ShareAsync([CanBeNull] RssMessageServiceModel model, CancellationToken token = default);
    }
}
