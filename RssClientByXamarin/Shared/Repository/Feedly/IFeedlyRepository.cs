using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Repository.Feedly
{
    public interface IFeedlyRepository
    {
        Task<IEnumerable<FeedlyRss>> SearchByQueryAsync(string query, CancellationToken token = default);
    }
}