#region

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

#endregion

namespace Shared.Repositories.Rss
{
    public interface IRssRepository
    {
        [NotNull]
        [TaskItemNotNull]
        Task<string> AddAsync([CanBeNull] string url, CancellationToken token = default);

        [NotNull]
        Task UpdateAsync([CanBeNull] RssDomainModel rssDomainModel, CancellationToken token = default);

        [NotNull]
        [TaskItemCanBeNull]
        Task<RssDomainModel> GetAsync([CanBeNull] string id, CancellationToken token = default);

        [NotNull]
        Task RemoveAsync([CanBeNull] string id, CancellationToken token = default);

        [NotNull]
        [TaskItemNotNull]
        Task<IEnumerable<RssDomainModel>> GetListAsync(CancellationToken token = default);
    }
}
