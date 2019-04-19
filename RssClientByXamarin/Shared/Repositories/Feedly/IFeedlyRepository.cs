#region

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Shared.Repositories.Rss;

#endregion

namespace Shared.Repositories.Feedly
{
    public interface IFeedlyRepository
    {
        [NotNull]
        [TaskItemCanBeNull]
        Task<IEnumerable<FeedlyRssDomainModel>> SearchByQueryAsync([CanBeNull] string query, CancellationToken token = default);
    }
}
