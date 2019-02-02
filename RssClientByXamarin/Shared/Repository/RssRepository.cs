using System;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using RssClient.Repository;
using Shared.Analitics.Rss;
using Shared.Api;
using Shared.Database;
using Shared.Database.Rss;

namespace Shared.Repository
{
    public class RssRepository : IRssRepository
    {
        private readonly RealmDatabase _database;
        private readonly RssLog _log;
        private readonly IRssApiClient _client;

        public RssRepository(RealmDatabase database, IRssApiClient client, RssLog log)
        {
            _database = database;
            _client = client;
            _log = log;

            Update();
        }

        public async void Update()
        {
            await Task.Run(() =>
            {
                using (var realm = RealmDatabase.OpenDatabase)
                {
                    var items = realm.All<RssModel>().OrderByDescending(w => w.CreationTime);

                    var dataItems = items.ToList().Select(w => new {w.Id, w.Rss, w.UpdateTime}).ToList();

                    foreach (var rssModel in dataItems)
                    {
                        if (!rssModel.UpdateTime.HasValue ||
                            (rssModel.UpdateTime.Value.Date - DateTime.Now).TotalMinutes > 5)
                        {
                            StartUpdateAllByInternet(rssModel.Rss, rssModel.Id);
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

                var itemId = await RealmDatabase.InsertInBackground(newItem);

                await StartUpdateAllByInternet(url, itemId);
            });
        }

        public Task Update(string id, string rss)
        {
            return Task.Run(async () =>
            {
                await RealmDatabase.UpdateInBackground<RssModel>(id, (model, realm) =>
                {
                    model.RssMessageModels.Clear();

                    _log.TrackRssUpdate(model.Rss, rss, model.Name, DateTimeOffset.Now);

                    model.Rss = rss;
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

            return RealmDatabase.DoInBackground(realm =>
            {
                var backgroundRssItem = realm.Find<RssModel>(id);
                var messages = backgroundRssItem.RssMessageModels;
                
                foreach (var rssMessageModel in messages)
                    realm.Remove(rssMessageModel);
                
                realm.Remove(backgroundRssItem);
            });
        }

        public IQueryable<RssModel> GetList()
        {
            return _database.MainThreadRealm.All<RssModel>().OrderBy(w => w.Position).ThenByDescending(w => w.CreationTime);
        }

        public Task Update(string rssId, SyndicationFeed feed)
        {
            return Task.Run(() =>
            {
                // TODO Добавить обработку незагруженного состояния, может стоит очистить
                if (feed == null)
                    return;

                RealmDatabase.DoInBackground(realm =>
                {
                    var currentItem = realm.Find<RssModel>(rssId);

                    currentItem.Name = feed.Title?.Text;
                    currentItem.UpdateTime = DateTime.Now;
                    //TODO сюда запихнуть фавикон
                    currentItem.UrlPreviewImage = feed.Links?.FirstOrDefault()?.Uri?.OriginalString + "/favicon.ico";

                    foreach (var syndicationItem in feed.Items)
                    {
                        var imageUri = syndicationItem.Links.FirstOrDefault(w =>
                                w.RelationshipType?.Equals("enclosure", StringComparison.InvariantCultureIgnoreCase) ==
                                true && w.MediaType?.Equals("image/jpeg", StringComparison.InvariantCultureIgnoreCase) == true)
                                ?.Uri?.OriginalString;

                        var url = syndicationItem.Links.FirstOrDefault(w =>
                                w.RelationshipType?.Equals("alternate", StringComparison.InvariantCultureIgnoreCase) == true)?.Uri
                            ?.OriginalString;

                        var item = new RssMessageModel()
                        {
                            SyndicationId = syndicationItem.Id,
                            Title = SafeTrim(syndicationItem.Title?.Text),
                            Text = SafeTrim(syndicationItem.Summary.Text),
                            CreationDate = syndicationItem.PublishDate.Date,
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

        public void UpdatePosition(RssModel model, int position)
        {
            _database.MainThreadRealm.Write(() =>
            {
                model.Position = position;
                _database.MainThreadRealm.Add(model, true);
            });
        }

        private string SafeTrim(string text)
        {
            return text?.Trim(' ', '\n', '\r');
        }
    }
}