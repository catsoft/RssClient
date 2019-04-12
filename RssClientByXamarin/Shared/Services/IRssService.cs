using System.Threading;
using System.Threading.Tasks;
using Shared.Repository.Rss;

namespace Shared.Services
{
    public interface IRssService
    {
        Task Create(string url, CancellationToken cancellationToken = default);
        
        Task<RssData> Find(string id, CancellationToken cancellationToken = default);

        Task Update(string id, string value, CancellationToken cancellationToken = default);
    }
}