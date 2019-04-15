using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Threading;
using System.Threading.Tasks;
using Shared.Repository.Rss;

namespace Shared.Services.Rss
{
    public interface IRssService
    {
        Task AddAsync(string url, CancellationToken cancellationToken = default);
        
        Task RemoveAsync(string id, CancellationToken token = default);
        
        Task<RssServiceModel> GetAsync(string id, CancellationToken token = default);

        Task<IEnumerable<RssServiceModel>> GetListAsync(CancellationToken token = default);
        
        Task UpdateAsync(RssServiceModel rss, CancellationToken token = default);

        Task LoadAndUpdateAsync(string id, CancellationToken token = default);
        
        Task UpdatePositionAsync(string localItemId, int position, CancellationToken token = default);
        
        Task ReadAllMessagesAsync(string itemId, CancellationToken token = default);
    }
}