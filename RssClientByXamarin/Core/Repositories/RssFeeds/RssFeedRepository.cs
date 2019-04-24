using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Analytics.Rss;
using Core.Database;
using Core.Database.Rss;
using Core.Infrastructure.Mappers;
using JetBrains.Annotations;

namespace Core.Repositories.RssFeeds
{
    public class RssFeedRepository : IRssFeedRepository
    {
        [NotNull] private readonly SqliteDatabase _sqliteDatabase;
        [NotNull] private readonly RssLog _log;
        [NotNull] private readonly IMapper<RssFeedModel, RssFeedDomainModel> _mapper;

        public RssFeedRepository([NotNull] SqliteDatabase sqliteDatabase, [NotNull] RssLog log, [NotNull] IMapper<RssFeedModel, RssFeedDomainModel> mapper)
        {
            _sqliteDatabase = sqliteDatabase;
            _log = log;
            _mapper = mapper;
        }

        public Task<Guid> AddAsync(string url, CancellationToken token = default)
        {
            return _sqliteDatabase.DoWithConnectionAsync(connection =>
            {
                var newItem = new RssFeedModel
                {
                    Id = Guid.NewGuid(),
                    Rss = url,
                    Name = url,
                    CreationTime = DateTime.Now
                };

                _log.TrackRssInsert(url);
                connection.Insert(newItem);
                
                return newItem.Id;
            }, token);
        }

        public Task UpdateAsync(RssFeedDomainModel rssFeedDomainModel, CancellationToken token = default)
        {
            if (rssFeedDomainModel == null) return Task.CompletedTask;

            return _sqliteDatabase.DoWithConnectionAsync((connection =>
            {
                var rss = connection.Find<RssFeedModel>(rssFeedDomainModel.Id);
                if (rss == null) return;

                rss.Rss = rssFeedDomainModel.Rss;
                rss.Name = rssFeedDomainModel.Name;
                rss.Position = rssFeedDomainModel.Position;
                rss.UpdateTime = rssFeedDomainModel.UpdateTime;
                rss.CreationTime = rssFeedDomainModel.CreationTime;
                rss.UrlPreviewImage = rssFeedDomainModel.UrlPreviewImage;

                connection.Update(rss);
            }), token);

        }

        public Task<RssFeedDomainModel> GetAsync(Guid id, CancellationToken token = default)
        {
            return _sqliteDatabase.DoWithConnectionAsync((connection) =>
            {
                var items = connection.Find<RssFeedModel>(id);
                return items == null ? null : _mapper.Transform(items);
            }, token);
        }

        public Task RemoveAsync(Guid id, CancellationToken token = default)
        {
            return _sqliteDatabase.DoWithConnectionAsync((connection) =>
            {
                var rssItem = connection.Find<RssFeedModel>(id);

                if (rssItem == null) return;

                _log.TrackRssDelete(rssItem.Rss);

                connection.Delete(rssItem);
            }, token);
        }

        public Task<IEnumerable<RssFeedDomainModel>> GetListAsync(CancellationToken token = default)
        {
            return _sqliteDatabase.DoWithConnectionAsync((connection) =>
            {
                var items = connection.Table<RssFeedModel>()
                    ?.OrderBy(w => w.Position)
                    .ThenByDescending(w => w.CreationTime)
                    .ToList()
                    .Select(_mapper.Transform)
                    .ToList();

                return items?.AsEnumerable() ?? new List<RssFeedDomainModel>();
            }, token);
        }
    }
}
