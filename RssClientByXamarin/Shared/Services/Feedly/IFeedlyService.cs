#region

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Shared.Repositories.Feedly;

#endregion

namespace Shared.Services.Feedly
{
    public interface IFeedlyService
    {
        Task<IEnumerable<FeedlyRssDomainModel>> FindByQueryAsync(string query, CancellationToken token = default);

        Task AddFeedly(FeedlyRssDomainModel model, CancellationToken token = default);
    }
}
