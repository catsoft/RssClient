using System;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using Database;
using Database.Rss;
using Shared.App.Rss.Log;
using Shared.App.Rss.RssDatabase;
using Shared.App.RssClient;

namespace Shared.App.Rss
{
    public class RssRepository
    {
        private static RssRepository _instance;
        public static RssRepository Instance => _instance ?? (_instance = new RssRepository());

        private readonly RealmDatabase _database;
        private readonly RssMessagesRepository _rssMessagesRepository;
        private RssLog _log => RssLog.Instance;
        private readonly RssApiClient _client;

        private RssRepository()
        {
            _database = RealmDatabase.Instance;
            _rssMessagesRepository = RssMessagesRepository.Instance;
            _client = RssApiClient.Instance;

            Init();
        }

        public async void Init()
        {
            await Task.Run(async () =>
            {
                using (var realm = _database.OpenDatabase)
                {
                    var items = realm.All<RssModel>().OrderByDescending(w => w.CreationTime);

                    if (!items.Any())
                    {
                        await InsertByUrl("https://meteoinfo.ru/rss/forecasts/index.php?s=28440");
                        await InsertByUrl("https://acomics.ru/~depth-of-delusion/rss");
                        await InsertByUrl("http://www.calend.ru/img/export/calend.rss");
                        await InsertByUrl("http://www.old-hard.ru/rss");
                        await InsertByUrl("https://lenta.ru/rss/news");
                        await InsertByUrl("https://bad_link.sad");
                        await InsertByUrl("https://lenta.ru/rss/articles");
                        await InsertByUrl("https://lenta.ru/rss/top7");
                        await InsertByUrl("https://lenta.ru/rss/news/russia");

                        Init();
                        return;
                    }

                    var dataItems = items.ToList().Select(w => new {w.Id, w.Rss, w.UpdateTime}).ToList();

                    foreach (var rssModel in dataItems)
                    {
                        if (!rssModel.UpdateTime.HasValue ||
                            (rssModel.UpdateTime.Value.Date - DateTime.Now).TotalMinutes > 5)
                        {
                            await StartUpdateAllByInternet(rssModel.Rss, rssModel.Id);
                        }
                    }
                }
            });
        }

        public async Task StartUpdateAllByInternet(string url, string id)
        {
            var request = await _client.Update(url);
            await Update(id, request);
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

                _log.TrackRssInsert(url, newItem.CreationTime);

                var itemId = await _database.InsertInBackground(newItem);

                await StartUpdateAllByInternet(url, itemId);
            });
        }

        public Task Update(string id, string rss, string name)
        {
            return Task.Run(async () =>
            {
                await _database.UpdateInBackground<RssModel>(id, model =>
                {
                    model.RssMessageModels.Clear();

                    _log.TrackRssUpdate(model.Rss, rss, model.Name, name, DateTimeOffset.Now);

                    model.Rss = rss;
                    model.Name = name;
                    model.UpdateTime = null;
                    model.UrlPreviewImage = null;
                });

                await StartUpdateAllByInternet(rss, id);
            });
        }

        public RssModel Find(string id)
        {
            return _database.MainThreadRealm.Find<RssModel>(id);
        }

        public Task Remove(RssModel item)
        {
            _log.TrackRssDelete(item.Rss, DateTimeOffset.Now);
            var id = item.Id;

            return _database.DoInBackground(realm => realm.Remove(realm.Find<RssModel>(id)));
        }

        public IQueryable<RssModel> GetList()
        {
            return _database.MainThreadRealm.All<RssModel>().OrderByDescending(w => w.CreationTime);
        }

        public Task Update(string rssId, SyndicationFeed feed)
        {
            return Task.Run(() =>
            {
                // TODO Добавить обработку незагруженного состояния, может стоит очистить
                if (feed == null)
                    return;

                _database.DoInBackground(realm =>
                {
                    var currentItem = realm.Find<RssModel>(rssId);

                    currentItem.Name = feed.Title?.Text;
                    currentItem.UpdateTime = DateTime.Now;
                    currentItem.UrlPreviewImage = feed.Links?.FirstOrDefault()?.Uri?.OriginalString + "/favicon.ico";

                    foreach (var syndicationItem in feed.Items)
                    {
                        var imageUri = syndicationItem.Links.FirstOrDefault(w =>
                            w.RelationshipType?.Equals("enclosure", StringComparison.InvariantCultureIgnoreCase) == true &&
                            w.MediaType?.Equals("image/jpeg", StringComparison.InvariantCultureIgnoreCase) == true)?.Uri?.OriginalString;

                        var url = syndicationItem.Links.FirstOrDefault(w => w.RelationshipType?.Equals("alternate", StringComparison.InvariantCultureIgnoreCase) == true)?.Uri
                            ?.OriginalString;

                        var item = new RssMessageModel()
                        {
                            SyndicationId = syndicationItem.Id,
                            Title = SafeTrim(syndicationItem.Title?.Text),
                            Text = SafeTrim(syndicationItem.Summary.Text),
                            //				CreationDate = syndicationItem.PublishDate.Date,
                            Url = url,
                            ImageUrl = imageUri,
                        };

                        var rssMessage = currentItem.RssMessageModels.FirstOrDefault(w => w.SyndicationId == item.SyndicationId);

                        if (rssMessage != null)
                            item.Id = rssMessage.Id;

                        if (rssMessage != null)
                        {
                            realm.Add(rssMessage, true);
                        }
                        else
                        {
                            currentItem.RssMessageModels.Add(item);
                        }
                    }
                });
            });
        }

        private string SafeTrim(string text)
        {
            return text?.Trim(' ', '\n', '\r');
        }
    }
}