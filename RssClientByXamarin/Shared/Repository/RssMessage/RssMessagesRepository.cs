using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Droid.Repository.Configuration;
using Shared.Configuration.Settings;
using Shared.Database;
using Shared.Database.Rss;
using Shared.Infrastructure.Mappers;

namespace Shared.Repository.RssMessage
{
    public class RssMessagesRepository : IRssMessagesRepository
    {
        private readonly RealmDatabase _localDatabase;
        private readonly IConfigurationRepository _configurationRepository;
        private readonly IMapper<RssMessageModel, RssMessageData> _mapper;
        public RssMessagesRepository(RealmDatabase localDatabase, IConfigurationRepository configurationRepository, IMapper<RssMessageModel, RssMessageData> mapper)
        {
            _localDatabase = localDatabase;
            _configurationRepository = configurationRepository;
            _mapper = mapper;
        }

        public RssMessageData FindById(string id, CancellationToken token)
        {
            return _mapper.Transform(_localDatabase.MainThreadRealm.Find<RssMessageModel>(id));
        }

        public async Task MarkAsFavoriteAsync(string id, CancellationToken token)
        {
            await RealmDatabase.UpdateInBackground<RssMessageModel>(id,(message, realm) => { message.IsFavorite = true; });
        }

        public async Task MarkAsReadAsync(string id, CancellationToken token)
        {
            await RealmDatabase.UpdateInBackground<RssMessageModel>(id, (message, realm) => { message.IsRead = true; });
        }

        public async Task ChangeIsFavoriteAsync(string id, CancellationToken token)
        {
            await RealmDatabase.UpdateInBackground<RssMessageModel>(id,(message, realm) => { message.IsFavorite = !message.IsFavorite; });
        }

        public async Task ChangeIsReadAsync(string id, CancellationToken token)
        {
            await RealmDatabase.UpdateInBackground<RssMessageModel>(id, (message, realm) => { message.IsRead = !message.IsRead; });
        }

        public IEnumerable<RssMessageData> GetMessagesForRss(string rssId, CancellationToken token)
        {
            var appConfiguration = _configurationRepository.GetSettings<AppConfiguration>();
            var hideReadMessages = appConfiguration.HideReadMessages;
            var rssModel = _localDatabase.MainThreadRealm.Find<RssModel>(rssId);
            IEnumerable<RssMessageModel> messages = rssModel.RssMessageModels;
            if (hideReadMessages)
                messages = messages.Where(w => !w.IsRead);
            return messages.OrderByDescending(w => w.CreationDate).ToList().Select(_mapper.Transform);
        }

        public long GetCountNewMessagesForModel(string rssId, CancellationToken token)
        {
            var rssModel = _localDatabase.MainThreadRealm.Find<RssModel>(rssId);
            
            return rssModel.RssMessageModels.Count(w => !w.IsRead);
        }

        public long GetCountForModel(string rssId, CancellationToken token)
        {
            return GetMessagesForRss(rssId, token).Count();
        }

        public IEnumerable<RssMessageData> GetAllMessages(CancellationToken token)
        {
            return GetAllMessagesInner(token).ToList().Select(_mapper.Transform);
        }

        public IEnumerable<RssMessageData> GetFavoriteMessages(CancellationToken token)
        {
            return GetAllMessagesInner(token).Where(w => w.IsFavorite).ToList().Select(_mapper.Transform);
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
        
        public IEnumerable<RssMessageData> GetAllFilterMessages(AllMessageFilterConfiguration filterConfiguration, CancellationToken token)
        {
            var messages = GetAllMessagesInner(token);

            messages = filterConfiguration.ApplyFilter(messages);
            messages = filterConfiguration.ApplyDateFilter(messages);

            return messages.ToList().Select(_mapper.Transform);
        }
    }
}
