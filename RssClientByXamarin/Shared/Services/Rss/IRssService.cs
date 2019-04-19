#region

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

#endregion

namespace Shared.Services.Rss
{
    public interface IRssService
    {
        [NotNull]
        Task AddAsync(string url, CancellationToken cancellationToken = default);

        [NotNull]
        Task RemoveAsync(string id, CancellationToken token = default);

        [NotNull]
        Task<RssServiceModel> GetAsync(string id, CancellationToken token = default);

        [NotNull]
        Task<IEnumerable<RssServiceModel>> GetListAsync(CancellationToken token = default);

        [NotNull]
        Task UpdateAsync(RssServiceModel rss, CancellationToken token = default);

        [NotNull]
        Task LoadAndUpdateAsync(string id, CancellationToken token = default);

        [NotNull]
        Task UpdatePositionAsync(string localItemId, int position, CancellationToken token = default);

        [NotNull]
        Task ReadAllMessagesAsync(string itemId, CancellationToken token = default);
    }
}
