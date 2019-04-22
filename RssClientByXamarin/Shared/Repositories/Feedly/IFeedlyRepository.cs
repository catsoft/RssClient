using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Shared.Repositories.Feedly
{
    public interface IFeedlyRepository
    {
        [NotNull]
        [ItemCanBeNull]
        Task<IEnumerable<FeedlyRssDomainModel>> SearchByQueryAsync([CanBeNull] string query, CancellationToken token = default);
    }
}
