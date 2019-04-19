using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Shared.Repository.Feedly;
using Shared.Repository.Rss;

namespace Shared.Services.Feedly
{
    public class FeedlyService : IFeedlyService
    {
        private readonly IFeedlyRepository _feedlyRepository;
        private readonly IRssRepository _rssRepository;

        public FeedlyService(IFeedlyRepository feedlyRepository, IRssRepository rssRepository)
        {
            _feedlyRepository = feedlyRepository;
            _rssRepository = rssRepository;
        }

        public Task<IEnumerable<FeedlyRssDomainModel>> FindByQueryAsync(string query, CancellationToken token = default) =>
            _feedlyRepository.SearchByQueryAsync(query, token);

        public async Task AddFeedly(FeedlyRssDomainModel model, CancellationToken token)
        {
            var rss = model.FeedId.TrimStart("feed/".ToArray());
            await _rssRepository.AddAsync(rss, token);
        }
    }
}