using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Shared.Repository.Feedly;

namespace Shared.Services
{
    public class FeedlyService : IFeedlyService
    {
        private readonly IFeedlyRepository _feedlyRepository;

        public FeedlyService(IFeedlyRepository feedlyRepository)
        {
            _feedlyRepository = feedlyRepository;
        }

        public Task<IEnumerable<FeedlyRss>> FindByQueryAsync(string query, CancellationToken token) =>
            _feedlyRepository.FindByQuery(query, token);
    }
}