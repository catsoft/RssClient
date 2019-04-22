using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Droid.Repositories.Configuration;
using JetBrains.Annotations;
using Realms;
using Shared.Configuration.Settings;
using Shared.Database;
using Shared.Database.Rss;
using Shared.Extensions;
using Shared.Infrastructure.Mappers;

namespace Shared.Repositories.RssMessage
{
    public class RssMessagesRepository : IRssMessagesRepository
    {
        [NotNull] private readonly IConfigurationRepository _configurationRepository;
        [NotNull] private readonly IMapper<RssMessageModel, RssMessageDomainModel> _mapperToData;
        [NotNull] private readonly IMapper<RssMessageDomainModel, RssMessageModel> _mapperToModel;

        public RssMessagesRepository(
            [NotNull] IConfigurationRepository configurationRepository,
            [NotNull] IMapper<RssMessageModel, RssMessageDomainModel> mapperToData,
            [NotNull] IMapper<RssMessageDomainModel, RssMessageModel> mapperToModel)
        {
            _configurationRepository = configurationRepository;
            _mapperToData = mapperToData;
            _mapperToModel = mapperToModel;
        }

        public async Task AddMessageAsync(RssMessageDomainModel messageDomainModel, string idRss, CancellationToken token = default)
        {
            var messageModel = _mapperToModel.Transform(messageDomainModel);

            await RealmDatabase.UpdateAsync<RssModel>(idRss,
                (rss, realm) =>
                {
                    var rssExistMessage = rss?.RssMessageModels?.FirstOrDefault(w => w?.SyndicationId == messageModel.SyndicationId);
                    messageModel.Id = rssExistMessage != null ? rssExistMessage.Id : Guid.NewGuid().ToString();

                    if (rssExistMessage != null)
                        realm.NotNull().Add(messageModel, true);
                    else
                        rss?.RssMessageModels?.Add(messageModel);
                });
        }

        public Task<RssMessageDomainModel> GetAsync(string id, CancellationToken token)
        {
            return Task.Run(() =>
                {
                    using (var realm = RealmDatabase.OpenDatabase)
                    {
                        var item = realm.Find<RssMessageModel>(id);
                        return item == null ? null : _mapperToData.Transform(item);
                    }
                },
                token);
        }

        public Task UpdateAsync(RssMessageDomainModel message, CancellationToken token)
        {
            if (message == null) return Task.CompletedTask;
            
            return RealmDatabase.DoInBackground(realm =>
            {
                var newModel = _mapperToModel.Transform(message);
                realm.NotNull().Add(newModel, true);
            });
        }

        public Task<IEnumerable<RssMessageDomainModel>> GetMessagesForRss(string rssId, CancellationToken token)
        {
            return Task.Run(() =>
                {
                    var appConfiguration = _configurationRepository.GetSettings<AppConfiguration>();
                    var hideReadMessages = appConfiguration.HideReadMessages;

                    using (var realm = RealmDatabase.OpenDatabase)
                    {
                        var rssModel = realm.Find<RssModel>(rssId);
                        var messages = rssModel?.RssMessageModels?.Where(w => w != null);
                        if (hideReadMessages)
                            messages = messages?.Where(w => !w.IsRead);
                        return messages?.OrderByDescending(w => w.NotNull().CreationDate)
                            .ToList()
                            .Select(_mapperToData.Transform)
                            .ToList()
                            .AsEnumerable();
                    }
                },
                token);
        }

        public Task<IEnumerable<RssMessageDomainModel>> GetAllMessages(CancellationToken token)
        {
            return Task.Run(() =>
                {
                    using (var realm = RealmDatabase.OpenDatabase)
                    {
                        return GetAllMessagesInner(realm).ToList().Select(_mapperToData.Transform).ToList().AsEnumerable();
                    }
                },
                token);
        }

        public Task<IEnumerable<RssMessageDomainModel>> GetAllFavoriteMessages(CancellationToken token)
        {
            return Task.Run(() =>
                {
                    using (var realm = RealmDatabase.OpenDatabase)
                    {
                        return GetAllMessagesInner(realm).Where(w => w.IsFavorite).ToList().Select(_mapperToData.Transform).ToList().AsEnumerable();
                    }
                },
                token);
        }

        public Task<IEnumerable<RssMessageDomainModel>> GetAllFilterMessages(AllMessageFilterConfiguration filterConfiguration, CancellationToken token)
        {
            return Task.Run(() =>
                {
                    using (var realm = RealmDatabase.OpenDatabase)
                    {
                        var messages = GetAllMessagesInner(realm);

                        messages = filterConfiguration?.ApplyFilter(messages);
                        messages = filterConfiguration?.ApplyDateFilter(messages);
                        messages = messages ?? new List<RssMessageModel>().AsQueryable();
                        
                        return messages.ToList().Select(_mapperToData.Transform);
                    }
                },
                token);
        }

        [NotNull]
        [ItemNotNull]
        private IQueryable<RssMessageModel> GetAllMessagesInner([NotNull] Realm realmDatabase)
        {
            var appConfiguration = _configurationRepository.GetSettings<AppConfiguration>();
            var hideReadMessages = appConfiguration.HideReadMessages;
            var messages = realmDatabase.All<RssMessageModel>();

            if (hideReadMessages)
                messages = messages?.Where(w => !w.IsRead);

            var filter = _configurationRepository.GetSettings<AllMessageFilterConfiguration>();

            messages = messages ?? new List<RssMessageModel>().AsQueryable();
            messages = filter.ApplySort(messages);

            return messages;
        }
        
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
