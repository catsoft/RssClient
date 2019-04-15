using System.Threading;
using System.Threading.Tasks;

namespace Shared.Api.Feedly
{
    public interface IFeedlyCloudApiClient
    {
        Task<FeedlyRssResponce> FindByQuery(string query, CancellationToken token = default);
    }
}