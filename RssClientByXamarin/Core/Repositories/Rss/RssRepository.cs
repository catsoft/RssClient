using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Analytics.Rss;
using Core.Database;
using Core.Database.Rss;
using Core.Extensions;
using Core.Infrastructure.Mappers;
using JetBrains.Annotations;

namespace Core.Repositories.Rss
{
    public class RssRepository : IRssRepository
    {
        [NotNull] private readonly RssLog _log;
        [NotNull] private readonly IMapper<RssModel, RssDomainModel> _mapper;

        public RssRepository([NotNull] RssLog log, [NotNull] IMapper<RssModel, RssDomainModel> mapper)
        {
            _log = log;
            _mapper = mapper;
        }

        public Task<string> AddAsync(string url, CancellationToken token = default)
        {
            return Task.Run(async () =>
                {
                    var newItem = new RssModel
                    {
                        Rss = url,
                        Name = url,
                        CreationTime = DateTime.Now
                    };

                    _log.TrackRssInsert(url);

                    var itemId = await RealmDatabase.InsertAsync(newItem);

                    return itemId;
                },
                token);
        }

        public Task UpdateAsync(RssDomainModel rssDomainModel, CancellationToken token = default)
        {
            if (rssDomainModel == null) return Task.CompletedTask;

            return RealmDatabase.DoInBackground(realm =>
            {
                var rss = realm.NotNull().Find<RssModel>(rssDomainModel.Id);
                if (rss == null) return;

                rss.Rss = rssDomainModel.Rss;
                rss.Name = rssDomainModel.Name;
                rss.Position = rssDomainModel.Position;
                rss.UpdateTime = rssDomainModel.UpdateTime;
                rss.CreationTime = rssDomainModel.CreationTime;
                rss.UrlPreviewImage = rssDomainModel.UrlPreviewImage;

                realm.NotNull();

                realm.NotNull().Add(rss, true);
            });
        }

        public Task<RssDomainModel> GetAsync(string id, CancellationToken token = default)
        {
            return Task.Run(() =>
                {
                    using (var realm = RealmDatabase.OpenDatabase)
                    {
                        var items = realm.Find<RssModel>(id);
                        return items == null ? null : _mapper.Transform(items);
                    }
                },
                token);
        }

        public Task RemoveAsync(string id, CancellationToken token = default)
        {
            return RealmDatabase.DoInBackground(realm =>
            {
                var backgroundRssItem = realm.NotNull().Find<RssModel>(id);

                if (backgroundRssItem == null) return;

                _log.TrackRssDelete(backgroundRssItem.Rss);

                backgroundRssItem.RssMessageModels?.Clear();
                realm.NotNull().Remove(backgroundRssItem);
            });
        }

        public Task<IEnumerable<RssDomainModel>> GetListAsync(CancellationToken token = default)
        {
            return Task.Run(() =>
                {
                    using (var realm = RealmDatabase.OpenDatabase)
                    {
                        var items = realm.All<RssModel>()
                            ?.OrderBy(w => w.Position)
                            .ThenByDescending(w => w.CreationTime)
                            .ToList()
                            .Select(_mapper.Transform)
                            .ToList();

                        return items?.AsEnumerable() ?? new List<RssDomainModel>();
                    }
                },
                token);
        }
    }
}
