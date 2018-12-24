using System;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using Database;
using Database.Rss;
using iOS.App.Rss.RssUpdater;
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
        private RssUpdater _rssUpdater => RssUpdater.Instance;

		private RssRepository()
		{
			_database = RealmDatabase.Instance;
			_rssMessagesRepository = RssMessagesRepository.Instance;
        }

		public Task InsertByUrl(string url)
        {
            return Task.Run(async () =>
            {
                var newItem = new RssModel()
                {
                    Rss = url,
                    Name = url,
                    CreationTime = DateTime.Now,
                };

                using (var realm = _database.OpenDatabase)
                {
                    using (var transaction = realm.BeginWrite())
                    {
                        newItem = realm.Add(newItem, true);
                        transaction.Commit();
                    }
                }

                await _rssUpdater.StartUpdateAllByInternet(newItem);
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

                        // TODO удалить связанные объекты
                        currentThreadItem.Rss = rss;
                        currentThreadItem.Name = name;
                        currentThreadItem.UpdateTime = null;

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
                        await _rssMessagesRepository.AddOrUpdateItem(syndicationItem, rssId);
                    }
                }
            });
        }
	}
}
