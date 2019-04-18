using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Repository.Feedly
{
    public interface IFeedlyRepository
    {
        Task<IEnumerable<FeedlyRssDomainModel>> SearchByQueryAsync(string query, CancellationToken token = default);
    }
}