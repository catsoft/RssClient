using System.ServiceModel.Syndication;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Api.Rss
{
    public interface IRssApiClient
    {
        Task<SyndicationFeed> Update(string rssUrl, CancellationToken token = default);
    }
}