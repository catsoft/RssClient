#region

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Shared.Repositories.Feedly;
using Shared.Repositories.Rss;

#endregion

namespace Shared.Services.Feedly
{
    public interface IFeedlyService
    {
        [NotNull]
        [TaskItemCanBeNull]
        Task<IEnumerable<FeedlyRssDomainModel>> FindByQueryAsync([CanBeNull] string query, CancellationToken token = default);

        [NotNull]
        Task AddFeedly([CanBeNull] FeedlyRssDomainModel model, CancellationToken token = default);
    }
}
