using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Shared.Api.Feedly
{
    public interface IFeedlyCloudApiClient
    {
        [NotNull]
        [ItemCanBeNull]
        Task<FeedlyRssResponce> FindByQueryAsync([CanBeNull] string query, CancellationToken token = default);
    }
}
