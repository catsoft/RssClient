using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Core.Repositories.RssFeeds
{
    public interface IRssFeedRepository
    {
        [NotNull]
        [ItemNotNull]
        Task<string> AddAsync([CanBeNull] string url, CancellationToken token = default);

        [NotNull]
        Task UpdateAsync([CanBeNull] RssFeedDomainModel rssFeedDomainModel, CancellationToken token = default);

        [NotNull]
        [ItemCanBeNull]
        Task<RssFeedDomainModel> GetAsync([CanBeNull] string id, CancellationToken token = default);

        [NotNull]
        Task RemoveAsync([CanBeNull] string id, CancellationToken token = default);

        [NotNull]
        [ItemNotNull]
        Task<IEnumerable<RssFeedDomainModel>> GetListAsync(CancellationToken token = default);
    }
}
