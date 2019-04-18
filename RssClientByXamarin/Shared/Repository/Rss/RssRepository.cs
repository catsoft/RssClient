using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Shared.Analitics.Rss;
using Shared.Database;
using Shared.Database.Rss;
using Shared.Infrastructure.Mappers;

namespace Shared.Repository.Rss
{
    public class RssRepository : IRssRepository
    {
        private readonly RssLog _log;
        private readonly IMapper<RssModel, RssDomainModel> _mapper;

        public RssRepository(RssLog log, IMapper<RssModel, RssDomainModel> mapper)
        {
            _log = log;
            _mapper = mapper;
        }

        public Task<string> AddAsync(string url, CancellationToken token = default)
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

                var itemId = await RealmDatabase.InsertAsync(newItem);

                return itemId;
            }, token);
        }

        public Task UpdateAsync(RssDomainModel rssDomainModel, CancellationToken token = default)
        {
            return RealmDatabase.DoInBackground(realm =>
            {
                var rss = realm.Find<RssModel>(rssDomainModel.Id);
                rss.Rss = rssDomainModel.Rss;
                rss.Name = rssDomainModel.Name;
                rss.Position = rssDomainModel.Position;
                rss.UpdateTime = rssDomainModel.UpdateTime;
                rss.CreationTime = rssDomainModel.CreationTime;
                rss.UrlPreviewImage = rssDomainModel.UrlPreviewImage;

                realm.Add(rss, true);
            });
        }

        public Task<RssDomainModel> GetAsync(string id, CancellationToken token = default)
        {
            return Task.Run(() =>
            {
                using (var realm = RealmDatabase.OpenDatabase)
                {
                    var items = realm.Find<RssModel>(id);
                    return _mapper.Transform(items);
                }
            }, token);
        }

        public Task RemoveAsync(string id, CancellationToken token = default)
        {
            return RealmDatabase.DoInBackground(realm =>
            {
                var backgroundRssItem = realm.Find<RssModel>(id);

                if (backgroundRssItem != null)
                {
                    _log.TrackRssDelete(backgroundRssItem.Rss, DateTimeOffset.Now);
                
                    backgroundRssItem.RssMessageModels.Clear();
                    realm.Remove(backgroundRssItem);
                }
            });
        }

        public Task<IEnumerable<RssDomainModel>> GetListAsync(CancellationToken token = default)
        {
            return Task.Run(() =>
            {
                using (var realm = RealmDatabase.OpenDatabase)
                {
                    var items = realm.All<RssModel>()
                        .OrderBy(w => w.Position)
                        .ThenByDescending(w => w.CreationTime)
                        .ToList()
                        .Select(_mapper.Transform)
                        .ToList();
                    
                    return items.AsEnumerable();
                }
            }, token);
        }
    }
}