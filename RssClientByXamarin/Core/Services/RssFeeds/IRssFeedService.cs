using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Core.Services.RssFeeds
{
    public interface IRssFeedService
    {
        [NotNull]
        Task AddAsync([CanBeNull] string url, CancellationToken cancellationToken = default);

        [NotNull]
        Task RemoveAsync([CanBeNull] string id, CancellationToken token = default);

        [NotNull]
        [ItemCanBeNull]
        Task<RssFeedServiceModel> GetAsync([CanBeNull] string id, CancellationToken token = default);

        [NotNull]
        [ItemNotNull]
        Task<IEnumerable<RssFeedServiceModel>> GetListAsync(CancellationToken token = default);

        [NotNull]
        Task UpdateAsync([CanBeNull] RssFeedServiceModel rssFeed, CancellationToken token = default);

        [NotNull]
        Task LoadAndUpdateAsync([CanBeNull] string id, CancellationToken token = default);

        [NotNull]
        Task UpdatePositionAsync([CanBeNull] string localItemId, int position, CancellationToken token = default);

        [NotNull]
        Task ReadAllMessagesAsync([CanBeNull] string itemId, CancellationToken token = default);

        [NotNull]
        Task ShareAsync([CanBeNull] RssFeedServiceModel model, CancellationToken token = default);
    }
}
