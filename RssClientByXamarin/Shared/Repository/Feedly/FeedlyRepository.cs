using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Shared.Api.Feedly;
using Shared.Infrastructure.Mappers;

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
        
        public async Task<IEnumerable<FeedlyRss>> SearchByQueryAsync(string query, CancellationToken token = default)
        {
            var items = await _feedlyCloudApiClient.FindByQueryAsync(query, token);

            return items.Results?.Select(_mapper.Transform);
        }
    }
}