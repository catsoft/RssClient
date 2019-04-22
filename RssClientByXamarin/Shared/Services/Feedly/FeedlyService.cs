using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Shared.Repositories.Feedly;
using Shared.Repositories.Rss;

namespace Shared.Services.Feedly
{
    public class FeedlyService : IFeedlyService
    {
        [NotNull] private readonly IFeedlyRepository _feedlyRepository;
        [NotNull] private readonly IRssRepository _rssRepository;

        public FeedlyService([NotNull] IFeedlyRepository feedlyRepository, [NotNull] IRssRepository rssRepository)
        {
            _feedlyRepository = feedlyRepository;
            _rssRepository = rssRepository;
        }

        public Task<IEnumerable<FeedlyRssDomainModel>> FindByQueryAsync(string query, CancellationToken token = default)
        {
            return _feedlyRepository.SearchByQueryAsync(query, token);
        }

        public async Task AddFeedly(FeedlyRssDomainModel model, CancellationToken token)
        {
            var rss = model?.FeedId?.TrimStart("feed/".ToArray());
            await _rssRepository.AddAsync(rss, token);
        }
    }
}
