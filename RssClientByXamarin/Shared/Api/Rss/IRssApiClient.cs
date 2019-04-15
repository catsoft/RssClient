using System.ServiceModel.Syndication;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Api.Rss
{
    public interface IRssApiClient
    {
        Task<SyndicationFeed> LoadFeedsAsync(string rssUrl, CancellationToken token = default);
    }
}