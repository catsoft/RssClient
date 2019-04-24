using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Configuration.AllMessageFilter;
using Core.Database;
using Core.Database.Rss;
using Core.Infrastructure.Mappers;
using Core.Repositories.Configurations;
using JetBrains.Annotations;

namespace Core.Repositories.RssMessage
{
    public class RssMessagesRepository : IRssMessagesRepository
    {
        [NotNull] private readonly SqliteDatabase _sqliteDatabase;
        [NotNull] private readonly IConfigurationRepository _configurationRepository;
        [NotNull] private readonly IMapper<RssMessageModel, RssMessageDomainModel> _mapperToData;
        [NotNull] private readonly IMapper<RssMessageDomainModel, RssMessageModel> _mapperToModel;

        public RssMessagesRepository(
            [NotNull] SqliteDatabase sqliteDatabase,
            [NotNull] IConfigurationRepository configurationRepository,
            [NotNull] IMapper<RssMessageModel, RssMessageDomainModel> mapperToData,
            [NotNull] IMapper<RssMessageDomainModel, RssMessageModel> mapperToModel)
        {
            _sqliteDatabase = sqliteDatabase;
            _configurationRepository = configurationRepository;
            _mapperToData = mapperToData;
            _mapperToModel = mapperToModel;
        }

        public Task AddMessageAsync(RssMessageDomainModel messageDomainModel, string idRss, CancellationToken token = default)
        {
            return _sqliteDatabase.DoWithConnectionAsync((connection) =>
            {
                var messageModel = _mapperToModel.Transform(messageDomainModel);
                
                connection.Insert(messageModel);
            }, token);
        }

        public Task<RssMessageDomainModel> GetAsync(string id, CancellationToken token)
        {
            return _sqliteDatabase.DoWithConnectionAsync((connection) =>
            {
                var item = connection.Find<RssMessageModel>(id);
                return item == null ? null : _mapperToData.Transform(item);
            }, token);
        }

        public Task UpdateAsync(RssMessageDomainModel message, CancellationToken token)
        {
            if (message == null) return Task.CompletedTask;
         
            return _sqliteDatabase.DoWithConnectionAsync((connection) =>
            {
                var newModel = _mapperToModel.Transform(message);
                connection.Update(newModel);
            }, token);
        }

        public Task<IEnumerable<RssMessageDomainModel>> GetMessagesForRss(string rssId, CancellationToken token)
        {
            return Task.Run(() =>
                {
//                    var appConfiguration = _configurationRepository.GetSettings<AppConfiguration>();
//                    var hideReadMessages = appConfiguration.HideReadMessages;
//
//                    using (var realm = RealmDatabase.OpenDatabase)
//                    {
//                        var rssModel = realm.Find<RssModel>(rssId);
//                        var messages = rssModel?.RssMessageModels?.Where(w => w != null);
//                        if (hideReadMessages)
//                            messages = messages?.Where(w => !w.IsRead);
//                        return messages?.OrderByDescending(w => w.NotNull().CreationDate)
//                            .ToList()
//                            .Select(_mapperToData.Transform)
//                            .ToList()
//                            .AsEnumerable();
//                    }
                    return new List<RssMessageDomainModel>().AsEnumerable();
                },
                token);
        }

        public Task<IEnumerable<RssMessageDomainModel>> GetAllMessages(CancellationToken token)
        {
            return Task.Run(() =>
                {
                    return new List<RssMessageDomainModel>().AsEnumerable();
                    
//                    using (var realm = RealmDatabase.OpenDatabase)
//                    {
//                        return GetAllMessagesInner(realm).ToList().Select(_mapperToData.Transform).ToList().AsEnumerable();
//                    }
                },
                token);
        }

        public Task<IEnumerable<RssMessageDomainModel>> GetAllFavoriteMessages(CancellationToken token)
        {
            return Task.Run(() =>
                {
                    return new List<RssMessageDomainModel>().AsEnumerable();

//                    
//                    using (var realm = RealmDatabase.OpenDatabase)
//                    {
//                        return GetAllMessagesInner(realm).Where(w => w.IsFavorite).ToList().Select(_mapperToData.Transform).ToList().AsEnumerable();
//                    }
                },
                token);
        }

        public Task<IEnumerable<RssMessageDomainModel>> GetAllFilterMessages(AllMessageFilterConfiguration filterConfiguration, CancellationToken token)
        {
            return Task.Run(() =>
                {
                    return new List<RssMessageDomainModel>().AsEnumerable();

                    
//                    using (var realm = RealmDatabase.OpenDatabase)
//                    {
//                        var messages = GetAllMessagesInner(realm);
//
//                        messages = filterConfiguration?.ApplyFilter(messages);
//                        messages = filterConfiguration?.ApplyDateFilter(messages);
//                        messages = messages ?? new List<RssMessageModel>().AsQueryable();
//                        
//                        return messages.ToList().Select(_mapperToData.Transform).ToList().AsEnumerable();
//                    }
                },
                token);
        }

//        [NotNull]
//        [ItemNotNull]
//        private IQueryable<RssMessageModel> GetAllMessagesInner()
//        {
//            var appConfiguration = _configurationRepository.GetSettings<AppConfiguration>();
//            var hideReadMessages = appConfiguration.HideReadMessages;
//            var messages = .All<RssMessageModel>();
//
//            if (hideReadMessages)
//                messages = messages?.Where(w => !w.IsRead);
//
//            var filter = _configurationRepository.GetSettings<AllMessageFilterConfiguration>();
//
//            messages = messages ?? new List<RssMessageModel>().AsQueryable();
//            messages = filter.ApplySort(messages);
//
//            return messages;
//        }
        
//        public long GetCountNewMessagesForModel(string rssId, CancellationToken token)
//        {
//            var rssModel = _localDatabase.mainThreadRealm.Find<RssModel>(rssId);
//            
//            return rssModel.RssMessageModels.Count(w => !w.IsRead);
//        }
//
//        public long GetCountForModel(string rssId, CancellationToken token)
//        {
//            return GetMessagesForRss(rssId, token).Count();
//        }
    }
}
