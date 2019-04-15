using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading;
using System.Threading.Tasks;
using Shared.Analitics.Rss;
using Shared.Api.Rss;
using Shared.Database;
using Shared.Database.Rss;
using Shared.Repository.RssMessage;
using Shared.Utils;

namespace Shared.Repository.Rss
{
    public class RssRepository : IRssRepository
    {
        private readonly RealmDatabase _database;
        private readonly RssLog _log;
        private readonly IRssApiClient _client;
        private readonly IRssMessagesRepository _rssMessagesRepository;
        private readonly IMapper<RssModel, RssData> _mapper;

        public RssRepository(RealmDatabase database, IRssApiClient client, RssLog log, IRssMessagesRepository rssMessagesRepository, IMapper<RssModel, RssData> mapper)
        {
            _database = database;
            _client = client;
            _log = log;
            _rssMessagesRepository = rssMessagesRepository;
            _mapper = mapper;
            
            Update();
        }

        public Task Update(CancellationToken token = default)
        {
            return Task.Run(() =>
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
            }, token);
        }

        public async Task StartUpdateAllByInternet(string url, string id, CancellationToken token = default)
        {
            var request = await _client.Update(url);
            await Update(id, request, token);
        }

        public Task InsertByUrl(string url, CancellationToken token = default)
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

                await StartUpdateAllByInternet(url, itemId, token);
            }, token);
        }

        public Task Update(string id, string rss, CancellationToken token = default)
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

                await StartUpdateAllByInternet(rss, id, token);
            }, token);
        } 

        //TODO async to do
        public async Task<RssData> Find(string id, CancellationToken token = default)
        {
            var rssData = _mapper.Transform(_database.MainThreadRealm.Find<RssModel>(id));

            await Task.Delay(1,token);

            return rssData;
        }

        public Task Remove(string id, CancellationToken token = default)
        {
            return RealmDatabase.DoInBackground(realm =>
            {
                var backgroundRssItem = realm.Find<RssModel>(id);
                
                _log.TrackRssDelete(backgroundRssItem.Rss, DateTimeOffset.Now);
                
                var messages = backgroundRssItem.RssMessageModels;
                
                foreach (var rssMessageModel in messages)
                    realm.Remove(rssMessageModel);
                
                realm.Remove(backgroundRssItem);
            });
        }

        //TODO async to do
        public Task<IEnumerable<RssData>> GetList(CancellationToken token = default)
        {
            var items = _database.MainThreadRealm.All<RssModel>().OrderBy(w => w.Position)
                .ThenByDescending(w => w.CreationTime).ToList().Select(_mapper.Transform);
            
            return Task.FromResult(items);
        }

        public Task Update(string rssId, SyndicationFeed feed, CancellationToken token = default)
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
                            Title = syndicationItem.Title?.Text?.SafeTrim(),
                            Text = syndicationItem.Summary.Text?.SafeTrim(),
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
            }, token);
        }

        public Task UpdatePosition(string id, int position, CancellationToken token = default)
        {
            RealmDatabase.UpdateInBackground<RssModel>(id, (model, realm) =>
            {
                model.Position = position;
                realm.Add(model, true);
            });
            
            return Task.CompletedTask;
        }

        public Task ReadAllMessages(string id, CancellationToken token = default)
        {
            RealmDatabase.UpdateInBackground<RssModel>(id, (model, realm) =>
            {
                foreach (var holderItemRssMessageModel in model.RssMessageModels)
                {
                    _rssMessagesRepository.MarkAsReadAsync(holderItemRssMessageModel.Id);
                }
            });
            
            return Task.CompletedTask;
        }
    }
}