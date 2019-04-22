using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Shared.Services.Rss
{
    public interface IRssService
    {
        [NotNull]
        Task AddAsync([CanBeNull] string url, CancellationToken cancellationToken = default);

        [NotNull]
        Task RemoveAsync([CanBeNull] string id, CancellationToken token = default);

        [NotNull]
        [ItemCanBeNull]
        Task<RssServiceModel> GetAsync([CanBeNull] string id, CancellationToken token = default);

        [NotNull]
        [ItemNotNull]
        Task<IEnumerable<RssServiceModel>> GetListAsync(CancellationToken token = default);

        [NotNull]
        Task UpdateAsync([CanBeNull] RssServiceModel rss, CancellationToken token = default);

        [NotNull]
        Task LoadAndUpdateAsync([CanBeNull] string id, CancellationToken token = default);

        [NotNull]
        Task UpdatePositionAsync([CanBeNull] string localItemId, int position, CancellationToken token = default);

        [NotNull]
        Task ReadAllMessagesAsync([CanBeNull] string itemId, CancellationToken token = default);
    }
}
