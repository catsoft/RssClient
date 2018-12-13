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

		private readonly IDatabase _database;
		private readonly RssMessagesRepository _rssMessagesRepository;

		private RssRepository()
		{
			_database = RealmDatabase.Instance;
			_rssMessagesRepository = RssMessagesRepository.Instance;;
		}

		public Task Insert(string url)
		{
			return Task.Run(() =>
			{
				var newItem = new RssModel()
				{
					Id = url,
					Name = url,
					CreationTime = DateTime.Now,
				};
				_database.AddOrUpdate(newItem);
			});
		}

		public Task Update(RssModel item, string rss, string name)
		{
			return Task.Run(async () =>
			{
				item.Name = name;

				if (item.Rss != rss)
				{
					await _rssMessagesRepository.DeleteItemsForRss(item);
					_database.Remove(item);

					item.Id = rss;
				}

				_database.AddOrUpdate(item);
			});
		}

		public Task Remove(RssModel item)
		{
			return Task.Run(() => _database.Remove(item));
		}

		public Task<IQueryable<RssModel>> GetList()
		{
			return Task.Run<IQueryable<RssModel>>(async () =>
			{
				var items = _database.All<RssModel>().OrderBy(w => w.CreationTime);

				if (!items.Any())
				{
					await Insert("https://meteoinfo.ru/rss/forecasts/index.php?s=28440");
					await Insert("https://acomics.ru/~depth-of-delusion/rss");
					await Insert("http://www.calend.ru/img/export/calend.rss");
					await Insert("http://www.old-hard.ru/rss");
					await Insert("https://lenta.ru/rss/news");
					await Insert("https://lenta.ru/rss/articles");
					await Insert("https://lenta.ru/rss/top7");
					await Insert("https://lenta.ru/rss/news/russia");
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

				_database.AddOrUpdate(item);

				foreach (var syndicationItem in feed.Items)
				{
					_rssMessagesRepository.AddItem(syndicationItem, item);
				}
			});
		}
	}
}
