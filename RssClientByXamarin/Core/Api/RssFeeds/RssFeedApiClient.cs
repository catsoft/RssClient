using System;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Core.Analytics;
using Core.Extensions;
using JetBrains.Annotations;

namespace Core.Api.RssFeeds
{
    public class RssFeedApiClient : HttpClient, IRssFeedApiClient
    {
        [NotNull] private readonly ILog _log;

        public RssFeedApiClient([NotNull] ILog log)
        {
            _log = log;
        }

        public async Task<SyndicationFeed> LoadFeedsAsync(string rssUrl, CancellationToken token = default)
        {
            try
            {
                var response = await GetAsync(rssUrl, token).NotNull();

                if (response?.Content != null)
                {
                    var stream = response.Content.ReadAsStreamAsync().Result.NotNull();
                    var xmlReader = XmlReader.Create(stream);
                    return SyndicationFeed.Load(xmlReader);
                }

                return null;
            }
            catch (Exception e)
            {
                _log.TrackLog(LogLevel.Warn, "UpdateFeed", "При попытке обновить данные", e);
                return null;
            }
        }
    }
}
