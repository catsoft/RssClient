using System;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using Database.Rss;

namespace Shared.App.RssClient
{
	public class RssApiClient : HttpClient
	{
		private static RssApiClient _instance;
		public static RssApiClient Instance => _instance ?? (_instance = new RssApiClient());

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
				return null;
			}
		}
	}
}
