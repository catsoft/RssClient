using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using Database;
using Database.Rss;
using Shared.App.Rss.RssDatabase;

namespace Shared.App.Rss
{
	public class RssRepository
	{
		private static RssRepository _instance;
		public static RssRepository Instance => _instance ?? (_instance = new RssRepository());

		private readonly LocalDb _localDatabase;
		private readonly RssMessagesRepository _rssMessagesRepository;

		private RssRepository()
		{
			_localDatabase = LocalDb.Instance;
			_rssMessagesRepository = RssMessagesRepository.Instance;;
		}

		public Task Insert(string url)
		{
			return Task.Run(async () =>
			{
				var newItem = new RssModel()
				{
					Id = url,
					Name = url,
					CreationTime = DateTime.Now,
				};
				_localDatabase.AddOrReplace(newItem);
			});
		}

		public Task Update(RssModel item, string rss, string name)
		{
			return Task.Run(async () =>
			{
				item.Name = name;

				if (item.Rss != rss)
				{
					await _rssMessagesRepository.DeleteItemForRss(item);
					_localDatabase.DeleteItemByLocalId(item);

					item.Id = rss;
					_localDatabase.AddOrReplace(item);
				}
				else
				{
					_localDatabase.UpdateItemByLocalId(item);
				}
			});
		}

		public Task Remove(RssModel item)
		{
			return Task.Run(() => _localDatabase.DeleteItemByLocalId(item));
		}

		public Task<List<RssModel>> GetList()
		{
			return Task.Run(async () =>
			{
				var items = _localDatabase.GetItems<RssModel>()?.OrderBy(w => w.CreationTime).ToList() ?? new List<RssModel>();

				if (items.Count == 0)
				{
					await Insert("https://meteoinfo.ru/rss/forecasts/index.php?s=28440");
					await Insert("https://acomics.ru/~depth-of-delusion/rss");
					await Insert("http://www.calend.ru/img/export/calend.rss");
					await Insert("http://www.old-hard.ru/rss");
					await Insert("https://lenta.ru/rss/news");
					await Insert("https://lenta.ru/rss/articles");
					await Insert("https://lenta.ru/rss/top7");
					await Insert("https://lenta.ru/rss/news/russia");

					items = _localDatabase.GetItems<RssModel>()?.OrderBy(w => w.CreationTime).ToList() ?? new List<RssModel>();
				}

				foreach (var rssModel in items)
				{
					rssModel.CountMessages = await _rssMessagesRepository.GetCountForRss(rssModel);
				}

				return items;
			});
		}

		public Task Update(RssModel item, SyndicationFeed feed)
		{
			return Task.Run(() =>
			{
				if (feed == null)
					return;

				item.Name = feed.Title?.Text;
				item.UpdateTime = DateTime.Now;
				item.UrlPreviewImage = feed.Links?.FirstOrDefault()?.Uri?.OriginalString + "/favicon.ico";

				_localDatabase.UpdateItemByLocalId(item);

				foreach (var syndicationItem in feed.Items)
				{
					_rssMessagesRepository.AddItem(syndicationItem, item);
				}
			});
		}
	}
}
