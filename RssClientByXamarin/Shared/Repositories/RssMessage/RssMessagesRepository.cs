#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Droid.Repositories.Configuration;
using Shared.Configuration.Settings;
using Shared.Database;
using Shared.Database.Rss;
using Shared.Infrastructure.Mappers;

#endregion

namespace Shared.Repositories.RssMessage
{
    public class RssMessagesRepository : IRssMessagesRepository
    {
        private readonly IConfigurationRepository _configurationRepository;
        private readonly RealmDatabase _localDatabase;
        private readonly IMapper<RssMessageModel, RssMessageDomainModel> _mapperToData;
        private readonly IMapper<RssMessageDomainModel, RssMessageModel> _mapperToModel;

        public RssMessagesRepository(
            RealmDatabase localDatabase,
            IConfigurationRepository configurationRepository,
            IMapper<RssMessageModel, RssMessageDomainModel> mapperToData,
            IMapper<RssMessageDomainModel, RssMessageModel> mapperToModel)
        {
            _localDatabase = localDatabase;
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
                    var rssExistMessage = rss.RssMessageModels.FirstOrDefault(w => w.SyndicationId == messageModel.SyndicationId);
                    messageModel.Id = rssExistMessage != null ? rssExistMessage.Id : Guid.NewGuid().ToString();

                    if (rssExistMessage != null)
                        realm.Add(messageModel, true);
                    else
                        rss.RssMessageModels.Add(messageModel);

                    rss.RssMessageModels.Add(messageModel);
                });
        }

        public Task<RssMessageDomainModel> GetAsync(string id, CancellationToken token)
        {
            return Task.Run(() =>
                {
                    using (var realm = RealmDatabase.OpenDatabase)
                    {
                        var item = realm.Find<RssMessageModel>(id);
                        return _mapperToData.Transform(item);
                    }
                },
                token);
        }

        public async Task MarkAsFavoriteAsync(string id, CancellationToken token)
        {
            await RealmDatabase.UpdateAsync<RssMessageModel>(id, (message, realm) => { message.IsFavorite = true; });
        }

        public async Task MarkAsReadAsync(string id, CancellationToken token)
        {
            await RealmDatabase.UpdateAsync<RssMessageModel>(id, (message, realm) => { message.IsRead = true; });
        }

        public async Task ChangeIsFavoriteAsync(string id, CancellationToken token)
        {
            await RealmDatabase.UpdateAsync<RssMessageModel>(id, (message, realm) => { message.IsFavorite = !message.IsFavorite; });
        }

        public async Task ChangeIsReadAsync(string id, CancellationToken token)
        {
            await RealmDatabase.UpdateAsync<RssMessageModel>(id, (message, realm) => { message.IsRead = !message.IsRead; });
        }

        public IEnumerable<RssMessageDomainModel> GetMessagesForRss(string rssId, CancellationToken token)
        {
            var appConfiguration = _configurationRepository.GetSettings<AppConfiguration>();
            var hideReadMessages = appConfiguration.HideReadMessages;
            var rssModel = _localDatabase.MainThreadRealm.Find<RssModel>(rssId);
            IEnumerable<RssMessageModel> messages = rssModel.RssMessageModels;
            if (hideReadMessages)
                messages = messages.Where(w => !w.IsRead);
            return messages.OrderByDescending(w => w.CreationDate).ToList().Select(_mapperToData.Transform);
        }

        public long GetCountNewMessagesForModel(string rssId, CancellationToken token = default) { return 1; }

        public long GetCountForModel(string rssId, CancellationToken token = default) { return 2; }

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

        public IEnumerable<RssMessageDomainModel> GetAllMessages(CancellationToken token)
        {
            return GetAllMessagesInner(token).ToList().Select(_mapperToData.Transform);
        }

        public IEnumerable<RssMessageDomainModel> GetFavoriteMessages(CancellationToken token)
        {
            return GetAllMessagesInner(token).Where(w => w.IsFavorite).ToList().Select(_mapperToData.Transform);
        }

        public IEnumerable<RssMessageDomainModel> GetAllFilterMessages(AllMessageFilterConfiguration filterConfiguration, CancellationToken token)
        {
            var messages = GetAllMessagesInner(token);

            messages = filterConfiguration.ApplyFilter(messages);
            messages = filterConfiguration.ApplyDateFilter(messages);

            return messages.ToList().Select(_mapperToData.Transform);
        }

        public Task UpdateAsync(RssMessageDomainModel message, CancellationToken token)
        {
            return RealmDatabase.DoInBackground(realm =>
            {
                var newModel = _mapperToModel.Transform(message);

                var messageModel = realm.Find<RssMessageModel>(message.Id);
                realm.Add(messageModel, true);
            });
        }

        private IQueryable<RssMessageModel> GetAllMessagesInner(CancellationToken token)
        {
            var appConfiguration = _configurationRepository.GetSettings<AppConfiguration>();
            var hideReadMessages = appConfiguration.HideReadMessages;
            var messages = _localDatabase.MainThreadRealm.All<RssMessageModel>();

            if (hideReadMessages)
                messages = messages.Where(w => !w.IsRead);

            var filter = _configurationRepository.GetSettings<AllMessageFilterConfiguration>();

            messages = filter.ApplySort(messages);

            return messages;
        }
    }
}
