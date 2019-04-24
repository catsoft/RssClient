using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Extensions;
using Core.Repositories.Feedly;
using Core.Repositories.RssFeeds;
using JetBrains.Annotations;

namespace Core.Services.Feedly
{
    public class FeedlySearchSearchService : IFeedlySearchService
    {
        [NotNull] private readonly IFeedlyRepository _feedlyRepository;
        [NotNull] private readonly IRssFeedRepository _rssFeedRepository;

        public FeedlySearchSearchService([NotNull] IFeedlyRepository feedlyRepository, [NotNull] IRssFeedRepository rssFeedRepository)
        {
            _feedlyRepository = feedlyRepository;
            _rssFeedRepository = rssFeedRepository;
        }

        public Task<IEnumerable<FeedlyRssDomainModel>> FindByQueryAsync(string query, CancellationToken token = default)
        {
            return _feedlyRepository.SearchByQueryAsync(query, token);
        }

        public async Task AddFeedly(FeedlyRssDomainModel model, CancellationToken token)
        {
            var rss = model?.FeedId?.TrimStart("feed/".ToArray());
            var guid = await _rssFeedRepository.AddAsync(rss, token);

            var item = (await _rssFeedRepository.GetAsync(guid, token)).NotNull();
            item.UrlPreviewImage = model?.IconUrl;
            await _rssFeedRepository.UpdateAsync(item, token);
        }
    }
}
