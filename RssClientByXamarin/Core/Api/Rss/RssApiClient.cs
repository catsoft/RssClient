using System;
using System.Globalization;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Core.Analytics;
using Core.Extensions;
using JetBrains.Annotations;

namespace Core.Api.Rss
{
    public class RssApiClient : IRssApiClient
    {
//        [NotNull] private readonly ILog _log;
//        [NotNull] private readonly HttpClient _httpClient;
//
//        public RssApiClient([NotNull] ILog log)
//        {
//            _log = log;
//            _httpClient = new HttpClient();
//        }

        public Task<SyndicationFeed> LoadFeedsAsync(string rssUrl, CancellationToken token = default)
        {
            return Task.FromResult<SyndicationFeed>(null);
//            try
//            {
//                var response = await _httpClient.GetAsync(rssUrl, token).NotNull();
//
//                if (response?.Content != null)
//                {
//                    var stream = response.Content.ReadAsStreamAsync().Result.NotNull();
//                    var xmlReader = XmlReader.Create(stream);
//                    return SyndicationFeed.Load(xmlReader);
//                }
//
//                return null;
//            }
//            catch (Exception e)
//            {
////                _log.TrackLog(LogLevel.Warn, "UpdateFeed", "При попытке обновить данные", e);
//                return null;
//            }
        }
    }
}
