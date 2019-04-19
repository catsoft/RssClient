#region

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace Shared.Repositories.Feedly
{
    public interface IFeedlyRepository
    {
        Task<IEnumerable<FeedlyRssDomainModel>> SearchByQueryAsync(string query, CancellationToken token = default);
    }
}
