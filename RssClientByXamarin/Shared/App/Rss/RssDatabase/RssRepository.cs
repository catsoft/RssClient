using System;
using System.Linq;
using System.ServiceModel.Syndication;
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

		public void Insert(string url)
		{
			var newItem = new RssModel()
			{
				Id = url,
				Name = url,
				CreationTime = DateTime.Now,
			};
			_database.AddOrUpdate(newItem);
		}

		public void Update(RssModel item, string rss, string name)
		{
			item.Name = name;

			if (item.Rss != rss)
			{
				_rssMessagesRepository.DeleteItemsForRss(item);
				_database.Remove(item);

				item.Id = rss;
			}

			_database.AddOrUpdate(item);
		}

		public void Remove(RssModel item)
		{
			_database.Remove(item);
		}

		public IQueryable<RssModel> GetList()
		{
			var items = _database.All<RssModel>().OrderByDescending(w => w.CreationTime);

			if (!items.Any())
			{
				Insert("https://meteoinfo.ru/rss/forecasts/index.php?s=28440");
				Insert("https://acomics.ru/~depth-of-delusion/rss");
				Insert("http://www.calend.ru/img/export/calend.rss");
				Insert("http://www.old-hard.ru/rss");
				Insert("https://lenta.ru/rss/news");
				Insert("https://lenta.ru/rss/articles");
				Insert("https://lenta.ru/rss/top7");
				Insert("https://lenta.ru/rss/news/russia");
			}

			return items;
		}

		public void Update(RssModel item, SyndicationFeed feed)
		{
			if (feed == null)
				return;

			using (var transaction = item.Realm.BeginWrite())
			{
				item.Name = feed.Title?.Text;
				item.UpdateTime = DateTime.Now;
				item.UrlPreviewImage = feed.Links?.FirstOrDefault()?.Uri?.OriginalString + "/favicon.ico";
				transaction.Commit();
			}

			foreach (var syndicationItem in feed.Items)
			{
				_rssMessagesRepository.AddItem(syndicationItem, item);
			}
		}
	}
}
