using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Api.Feedly;
using Core.Infrastructure.Mappers;
using JetBrains.Annotations;

namespace Core.Repositories.Feedly
{
    public class FeedlyRepository : IFeedlyRepository
    {
        [NotNull] private readonly IFeedlyCloudApiClient _feedlyCloudApiClient;
        [NotNull] private readonly IMapper<FeedlyRssApiModel, FeedlyRssDomainModel> _mapper;

        public FeedlyRepository([NotNull] IFeedlyCloudApiClient feedlyCloudApiClient,
            [NotNull] IMapper<FeedlyRssApiModel, FeedlyRssDomainModel> mapper)
        {
            _feedlyCloudApiClient = feedlyCloudApiClient;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FeedlyRssDomainModel>> SearchByQueryAsync(string query, CancellationToken token = default)
        {
            var items = await _feedlyCloudApiClient.FindByQueryAsync(query, token);

            return items.Results?.Select(_mapper.Transform);
        }
    }
}
