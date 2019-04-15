using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Repository.Rss
{
    public interface IRssRepository
    {
        Task<string> AddAsync(string url, CancellationToken token = default);
        
        Task UpdateAsync(RssDomainModel rssDomainModel, CancellationToken token = default);
        
        Task<RssDomainModel> GetAsync(string id, CancellationToken token = default);
        
        Task RemoveAsync(string id, CancellationToken token = default);
        
        Task<IEnumerable<RssDomainModel>> GetListAsync(CancellationToken token = default);
    }
}