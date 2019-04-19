#region

using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Shared.Repositories.Rss;

#endregion

namespace Shared.Api.Feedly
{
    public interface IFeedlyCloudApiClient
    {
        [NotNull]
        [TaskItemCanBeNull]
        Task<FeedlyRssResponce> FindByQueryAsync([CanBeNull] string query, CancellationToken token = default);
    }
}
