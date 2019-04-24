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
        [NotNull] private readonly SqliteDatabase _sqliteDatabase;
        [NotNull] private readonly RssLog _log;
        [NotNull] private readonly IMapper<RssModel, RssDomainModel> _mapper;

        public RssRepository([NotNull] SqliteDatabase sqliteDatabase, [NotNull] RssLog log, [NotNull] IMapper<RssModel, RssDomainModel> mapper)
        {
            _sqliteDatabase = sqliteDatabase;
            _log = log;
            _mapper = mapper;
        }

        public Task<string> AddAsync(string url, CancellationToken token = default)
        {
            return Task.Run(() =>
                {
                    var newItem = new RssModel
                    {
                        Rss = url,
                        Name = url,
                        CreationTime = DateTime.Now
                    };

                    _log.TrackRssInsert(url);

                    _sqliteDatabase.Connection.Insert(newItem);

                    return newItem.Id;
                },
                token);
        }

        public Task UpdateAsync(RssDomainModel rssDomainModel, CancellationToken token = default)
        {
            if (rssDomainModel == null) return Task.CompletedTask;

            return Task.Run(() =>
                {
                    var rss = _sqliteDatabase.Connection.Find<RssModel>(rssDomainModel.Id);
                    if (rss == null) return;

                    rss.Rss = rssDomainModel.Rss;
                    rss.Name = rssDomainModel.Name;
                    rss.Position = rssDomainModel.Position;
                    rss.UpdateTime = rssDomainModel.UpdateTime;
                    rss.CreationTime = rssDomainModel.CreationTime;
                    rss.UrlPreviewImage = rssDomainModel.UrlPreviewImage;

                    _sqliteDatabase.Connection.Update(rss);
                    _sqliteDatabase.Connection.Commit();
                },
                token);
        }

        public Task<RssDomainModel> GetAsync(string id, CancellationToken token = default)
        {
            return Task.Run(() =>
                {
                    var items = _sqliteDatabase.Connection.Find<RssModel>(id);
                    return items == null ? null : _mapper.Transform(items);
                },
                token);
        }

        public Task RemoveAsync(string id, CancellationToken token = default)
        {
            return Task.Run(() =>
            {
                var rssItem = _sqliteDatabase.Connection.Find<RssModel>(id);

                if (rssItem == null) return;

                _log.TrackRssDelete(rssItem.Rss);

                //todo удалить сообщения исчо
                _sqliteDatabase.Connection.Delete(id);
            }, token);
        }

        public Task<IEnumerable<RssDomainModel>> GetListAsync(CancellationToken token = default)
        {
            return Task.Run(() =>
                {
                    var items = _sqliteDatabase.Connection.Table<RssModel>()
                        ?.OrderBy(w => w.Position)
                        .ThenByDescending(w => w.CreationTime)
                        .ToList()
                        .Select(_mapper.Transform)
                        .ToList();

                    return items?.AsEnumerable() ?? new List<RssDomainModel>();
                },
                token);
        }
    }
}
