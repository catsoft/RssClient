using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Shared.Repositories.Rss
{
    public interface IRssRepository
    {
        [NotNull]
        [ItemNotNull]
        Task<string> AddAsync([CanBeNull] string url, CancellationToken token = default);

        [NotNull]
        Task UpdateAsync([CanBeNull] RssDomainModel rssDomainModel, CancellationToken token = default);

        [NotNull]
        [ItemCanBeNull]
        Task<RssDomainModel> GetAsync([CanBeNull] string id, CancellationToken token = default);

        [NotNull]
        Task RemoveAsync([CanBeNull] string id, CancellationToken token = default);

        [NotNull]
        [ItemNotNull]
        Task<IEnumerable<RssDomainModel>> GetListAsync(CancellationToken token = default);
    }
}
