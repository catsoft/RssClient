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

		public Task InsertByUrl(string url)
        {
            return Task.Run(() =>
            {
                var newItem = new RssModel()
                {
                    Id = url,
                    Name = url,
                    CreationTime = DateTime.Now,
                };

                using (var realm = _database.OpenDatabase)
                {
                    using (var transaction = realm.BeginWrite())
                    {
                        realm.Add(newItem, true);
                        transaction.Commit();
                    }
                }
            });
        }

		public Task Update(string rssId, string rss, string name)
        {
            return Task.Run(() =>
            {
                using (var realm = RealmDatabase.Instance.OpenDatabase)
                {
                    using (var transaction = realm.BeginWrite())
                    {
                        var currentThreadItem = realm.Find<RssModel>(rssId);

                        if (currentThreadItem.Rss != rss)
                        {
                            var copyItem = new RssModel();
                            copyItem.Id = rss;
                            copyItem.Name = name;
                            copyItem.CreationTime = currentThreadItem.CreationTime;

                            var items = realm.All<RssMessageModel>().Where(w => w.Id == currentThreadItem.Id);
                            realm.RemoveRange(items);

                            realm.Remove(currentThreadItem);
                            realm.Add(copyItem, true);
                        }
                        else
                        {
                            currentThreadItem.Name = name;
                            realm.Add(currentThreadItem, true);
                        }
                        transaction.Commit();
                    }
                }
            });
        }

		public RssModel Find(string id)
		{
			return _database.MainThreadRealm.Find<RssModel>(id);
		}

		public void Remove(RssModel item)
		{
			_database.MainThreadRealm.Write(() =>
			{
				_database.MainThreadRealm.Remove(item);
			});
		}

		public IQueryable<RssModel> GetList()
		{
			var items = _database.MainThreadRealm.All<RssModel>().OrderByDescending(w => w.CreationTime);

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

		public Task Update(string rssId, SyndicationFeed feed)
		{
			if (feed == null)
				return null;

            return Task.Run(async () =>
            {
                using (var thread = RealmDatabase.Instance.OpenDatabase)
                {
                    var currentItem = thread.Find<RssModel>(rssId);

                    using (var transaction = currentItem.Realm.BeginWrite())
                    {
                        currentItem.Name = feed.Title?.Text;
                        currentItem.UpdateTime = DateTime.Now;
                        currentItem.UrlPreviewImage = feed.Links?.FirstOrDefault()?.Uri?.OriginalString + "/favicon.ico";
                        transaction.Commit();
                    }

                    foreach (var syndicationItem in feed.Items)
                    {
                        await _rssMessagesRepository.AddItem(syndicationItem, rssId);
                    }
                }
            });
        }
	}
}
