using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Extensions;
using Core.Repositories.Feedly;
using Core.Services.RssFeeds;
using JetBrains.Annotations;

namespace Core.Services.Feedly
{
    public class FeedlySearchService : IFeedlySearchService
    {
        [NotNull] private readonly IFeedlyRepository _feedlyRepository;
        [NotNull] private readonly IRssFeedService _rssFeedService;

        public FeedlySearchService([NotNull] IFeedlyRepository feedlyRepository, [NotNull] IRssFeedService rssFeedService)
        {
            _feedlyRepository = feedlyRepository;
            _rssFeedService = rssFeedService;
        }

        public Task<IEnumerable<FeedlyRssDomainModel>> FindByQueryAsync(string query, CancellationToken token = default)
        {
            return _feedlyRepository.SearchByQueryAsync(query, token);
        }

        public async Task AddFeedly(FeedlyRssDomainModel model, CancellationToken token)
        {
            var rss = model?.FeedId?.TrimStart("feed/".ToArray());
            var guid = await _rssFeedService.AddAsync(rss, token);

            var item = (await _rssFeedService.GetAsync(guid, token)).NotNull();
            item.UrlPreviewImage = model?.IconUrl;
            item.IsFeedly = true;
            await _rssFeedService.UpdateAsync(item, token);
        }
    }
}
