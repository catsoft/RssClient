using System.ServiceModel.Syndication;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Core.Api.RssFeeds
{
    public interface IRssFeedApiClient
    {
        [NotNull]
        Task<SyndicationFeed> LoadFeedsAsync([CanBeNull] string rssUrl, CancellationToken token = default);
    }
}
