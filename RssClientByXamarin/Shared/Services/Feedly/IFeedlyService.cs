using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FeedlyRss = Shared.Repository.Feedly.FeedlyRss;

namespace Shared.Services.Feedly
{
    public interface IFeedlyService
    {
        Task<IEnumerable<FeedlyRss>> FindByQueryAsync(string query, CancellationToken token = default);
    }
}