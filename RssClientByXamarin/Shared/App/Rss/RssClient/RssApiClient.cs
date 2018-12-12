using System;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using Analytics;
using Database.Rss;

namespace Shared.App.RssClient
{
	public class RssApiClient : HttpClient
	{
		private static RssApiClient _instance;
		public static RssApiClient Instance => _instance ?? (_instance = new RssApiClient());

		private ILog _log;
		public RssApiClient()
		{
			_log = new Log();
		}

		public async Task<SyndicationFeed> Update(RssModel item)
		{
			try
			{
				var response = await GetAsync(item.Rss);

				var stream = response.Content.ReadAsStreamAsync();
				var xmlReader = XmlReader.Create(stream.Result);
				var feed = SyndicationFeed.Load(xmlReader);

				return feed;
			}
			catch (Exception e)
			{
				_log.TrackLog(LogLevel.Warn, "UpdateFeed", "При попытке обновить данные", e);
				return null;
			}
		}
	}
}
