using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Repositories.Feedly;
using JetBrains.Annotations;

namespace Core.Services.Feedly
{
    public interface IFeedlyService
    {
        [NotNull]
        [ItemCanBeNull]
        Task<IEnumerable<FeedlyRssDomainModel>> FindByQueryAsync([CanBeNull] string query, CancellationToken token = default);

        [NotNull]
        Task AddFeedly([CanBeNull] FeedlyRssDomainModel model, CancellationToken token = default);
    }
}
