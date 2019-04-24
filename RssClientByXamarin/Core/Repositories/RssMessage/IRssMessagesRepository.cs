using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Configuration.AllMessageFilter;
using Core.Database.Rss;
using JetBrains.Annotations;

namespace Core.Repositories.RssMessage
{
    public interface IRssMessagesRepository
    {
        [NotNull]
        Task AddAsync([CanBeNull] RssMessageDomainModel messageDomainModel, Guid idRss, CancellationToken token = default);

        [NotNull]
        [ItemCanBeNull]
        Task<RssMessageDomainModel> GetAsync(Guid id, CancellationToken token = default);

        [NotNull]
        Task UpdateAsync([CanBeNull] RssMessageDomainModel message, CancellationToken token = default);

        [NotNull]
        [ItemNotNull]
        Task<IEnumerable<RssMessageDomainModel>> GetMessagesForRss(Guid rssId, CancellationToken token = default);

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
        
        [NotNull]
        Task DeleteRssFeedMessages(Guid id, CancellationToken token = default);

        [NotNull]
        [ItemCanBeNull]
        Task<RssMessageDomainModel> GetMessageBySyndicationIdAsync(string syndicationId, Guid rssId, CancellationToken token);
    }
}
