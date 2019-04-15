using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Shared.Api.Feedly;

namespace Shared.Repository.Feedly
{
    public class FeedlyRepository : IFeedlyRepository
    {
        private readonly IFeedlyCloudApiClient _feedlyCloudApiClient;
        private readonly IMapper<Api.Feedly.FeedlyRss, FeedlyRss> _mapper;

        public FeedlyRepository(IFeedlyCloudApiClient feedlyCloudApiClient, IMapper<Api.Feedly.FeedlyRss, FeedlyRss> mapper)
        {
            _feedlyCloudApiClient = feedlyCloudApiClient;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<FeedlyRss>> FindByQuery(string query, CancellationToken token)
        {
            var items = await _feedlyCloudApiClient.FindByQuery(query, token);

            return items.Results.Select(_mapper.Transform);
        }
    }
}