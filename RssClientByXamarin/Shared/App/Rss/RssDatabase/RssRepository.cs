using System;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using Database;
using Database.Rss;
using Realms;
using Shared.App.Rss.RssDatabase;

namespace Shared.App.Rss
{
	public class RssRepository
	{
		private static RssRepository _instance;
		public static RssRepository Instance => _instance ?? (_instance = new RssRepository());

		private readonly RealmDatabase _database;
		private readonly RssMessagesRepository _rssMessagesRepository;

		private RssRepository()
		{
			_database = RealmDatabase.Instance;
			_rssMessagesRepository = RssMessagesRepository.Instance;;
		}

		public void InsertByUrl(string url)
		{
			var newItem = new RssModel()
			{
				Id = url,
				Name = url,
				CreationTime = DateTime.Now,
			};

			_database.Realm.Write(() =>
			{
				_database.Realm.Add(newItem, true);
			});
		}

		public void Update(RssModel item, string rss, string name)
		{
			if (item.Rss != rss)
			{
				var copyItem = new RssModel();
				copyItem.Id = rss;
				copyItem.Name = name;
				copyItem.CreationTime = item.CreationTime;

				_database.Realm.Write(() =>
				{
					var items = _database.Realm.All<RssMessageModel>().Where(w => w.Id == item.Id);
					_database.Realm.RemoveRange(items);

					_database.Realm.Remove(item);
					_database.Realm.Add(copyItem, true);
				});
			}
			else
			{
				_database.Realm.Write(() =>
				{
					item.Name = name;
					_database.Realm.Add(item, true);
				});
			}
		}

		public RssModel Find(string id)
		{
			return _database.Realm.Find<RssModel>(id);
		}

		public void Remove(RssModel item)
		{
			_database.Realm.Write(() =>
			{
				_database.Realm.Remove(item);
			});
		}

		public IQueryable<RssModel> GetList()
		{
			var items = _database.Realm.All<RssModel>().OrderByDescending(w => w.CreationTime);

			if (!items.Any())
			{
				InsertByUrl("https://meteoinfo.ru/rss/forecasts/index.php?s=28440");
				InsertByUrl("https://acomics.ru/~depth-of-delusion/rss");
				InsertByUrl("http://www.calend.ru/img/export/calend.rss");
				InsertByUrl("http://www.old-hard.ru/rss");
				InsertByUrl("https://lenta.ru/rss/news");
				InsertByUrl("https://lenta.ru/rss/articles");
				InsertByUrl("https://lenta.ru/rss/top7");
				InsertByUrl("https://lenta.ru/rss/news/russia");
			}

			return items;
		}

		public Task Update(RssModel item, SyndicationFeed feed)
		{
			if (feed == null)
				return null;

            return Task.Run(async () =>
            {
                using (var transaction = item.Realm.BeginWrite())
                {
                    item.Name = feed.Title?.Text;
                    item.UpdateTime = DateTime.Now;
                    item.UrlPreviewImage = feed.Links?.FirstOrDefault()?.Uri?.OriginalString + "/favicon.ico";
                    transaction.Commit();
                }

                foreach (var syndicationItem in feed.Items)
                {
                    await _rssMessagesRepository.AddItem(syndicationItem, item);
                }
            });
        }
	}
}
