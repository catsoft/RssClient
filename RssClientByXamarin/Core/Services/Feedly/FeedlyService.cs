using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Repositories.Feedly;
using Core.Repositories.RssFeeds;
using JetBrains.Annotations;

namespace Core.Services.Feedly
{
    public class FeedlyService : IFeedlyService
    {
        [NotNull] private readonly IFeedlyRepository _feedlyRepository;
        [NotNull] private readonly IRssFeedRepository _rssFeedRepository;

        public FeedlyService([NotNull] IFeedlyRepository feedlyRepository, [NotNull] IRssFeedRepository rssFeedRepository)
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
            await _rssFeedRepository.AddAsync(rss, token);
        }
    }
}
