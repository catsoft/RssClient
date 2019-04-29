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

namespace Core.Repositories.RssFeeds
{
    public class RssFeedRepository : IRssFeedRepository
    {
        [NotNull] private readonly SqliteDatabase _sqliteDatabase;
        [NotNull] private readonly RssLog _log;
        [NotNull] private readonly IMapper<RssFeedModel, RssFeedDomainModel> _mapperToDomain;
        [NotNull] private readonly IMapper<RssFeedDomainModel, RssFeedModel> _mapperToModel;

        public RssFeedRepository([NotNull] SqliteDatabase sqliteDatabase, 
            [NotNull] RssLog log,
            [NotNull] IMapper<RssFeedModel, RssFeedDomainModel> mapperToDomain,
            [NotNull] IMapper<RssFeedDomainModel, RssFeedModel> mapperToModel)
        {
            _sqliteDatabase = sqliteDatabase;
            _log = log;
            _mapperToDomain = mapperToDomain;
            _mapperToModel = mapperToModel;
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
                    connection.NotNull().Insert(newItem);

                    return newItem.Id;
                },
                token);
        }

        public Task UpdateAsync(RssFeedDomainModel rssFeedDomainModel, CancellationToken token = default)
        {
            if (rssFeedDomainModel == null) return Task.CompletedTask;

            return _sqliteDatabase.DoWithConnectionAsync(connection =>
                {
                    var item = _mapperToModel.Transform(rssFeedDomainModel);

                    connection.NotNull().Update(item);
                },
                token);
        }

        public Task<RssFeedDomainModel> GetAsync(Guid id, CancellationToken token = default)
        {
            return _sqliteDatabase.DoWithConnectionAsync(connection =>
                {
                    var item = connection.NotNull().Find<RssFeedModel>(id);
                    item = CountMessages(item);
                    return item == null ? null : _mapperToDomain.Transform(item);
                },
                token);
        }

        public Task RemoveAsync(Guid id, CancellationToken token = default)
        {
            return _sqliteDatabase.DoWithConnectionAsync(connection =>
                {
                    var rssItem = connection.NotNull().Find<RssFeedModel>(id);

                    if (rssItem == null) return;

                    _log.TrackRssDelete(rssItem.Rss);

                    connection.NotNull().Delete(rssItem);
                },
                token);
        }

        public Task<IEnumerable<RssFeedDomainModel>> GetListAsync(CancellationToken token = default)
        {
            return _sqliteDatabase.DoWithConnectionAsync(connection =>
                {
                    var items = connection.NotNull()
                        .Table<RssFeedModel>()
                        ?.OrderBy(w => w.Position)
                        ?.ThenByDescending(w => w.CreationTime)
                        ?.ToList()
                        .Select(CountMessages)
                        .Select(_mapperToDomain.Transform)
                        .ToList();

                    return items?.AsEnumerable() ?? new List<RssFeedDomainModel>();
                },
                token);
        }

        private RssFeedModel CountMessages(RssFeedModel rssFeedModel)
        {
            _sqliteDatabase.DoWithConnection((connection) =>
            {
                var allMessages = connection.Table<RssMessageModel>()?.Where(w => w.RssId == rssFeedModel.Id).Count();
                var countNewMessages = connection.Table<RssMessageModel>()?.Where(w => w.RssId == rssFeedModel.Id && !w.IsRead).Count();

                rssFeedModel.CountAllMessages = allMessages ?? 0;
                rssFeedModel.CountNewMessages = countNewMessages ?? 0;
            });
            
            return rssFeedModel;
        }
    }
}
