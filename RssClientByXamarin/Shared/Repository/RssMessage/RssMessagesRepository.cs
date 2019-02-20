using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Droid.Repository.Configuration;
using Shared.Configuration.Settings;
using Shared.Database;
using Shared.Database.Rss;

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

        public RssMessageData FindById(string id)
        {
            return _mapper.Transform(_localDatabase.MainThreadRealm.Find<RssMessageModel>(id));
        }

        public async Task MarkAsFavoriteAsync(string id)
        {
            await RealmDatabase.UpdateInBackground<RssMessageModel>(id,(message, realm) => { message.IsFavorite = true; });
        }

        public async Task MarkAsReadAsync(string id)
        {
            await RealmDatabase.UpdateInBackground<RssMessageModel>(id, (message, realm) => { message.IsRead = true; });
        }

        public async Task ChangeIsFavoriteAsync(string id)
        {
            await RealmDatabase.UpdateInBackground<RssMessageModel>(id,(message, realm) => { message.IsFavorite = !message.IsFavorite; });
        }

        public async Task ChangeIsReadAsync(string id)
        {
            await RealmDatabase.UpdateInBackground<RssMessageModel>(id, (message, realm) => { message.IsRead = !message.IsRead; });
        }

        public IEnumerable<RssMessageData> GetMessagesForRss(string rssId)
        {
            var appConfiguration = _configurationRepository.GetSettings<AppConfiguration>();
            var hideReadMessages = appConfiguration.HideReadMessages;
            var rssModel = _localDatabase.MainThreadRealm.Find<RssModel>(rssId);
            IEnumerable<RssMessageModel> messages = rssModel.RssMessageModels;
            if (hideReadMessages)
                messages = messages.Where(w => !w.IsRead);
            return messages.OrderByDescending(w => w.CreationDate).ToList().Select(_mapper.Transform);
        }

        public long GetCountNewMessagesForModel(string rssId)
        {
            var rssModel = _localDatabase.MainThreadRealm.Find<RssModel>(rssId);
            
            return rssModel.RssMessageModels.Count(w => !w.IsRead);
        }

        public long GetCountForModel(string rssId)
        {
            return GetMessagesForRss(rssId).Count();
        }

        public IEnumerable<RssMessageData> GetAllMessages()
        {
            return GetAllMessagesInner().ToList().Select(_mapper.Transform);
        }

        public IEnumerable<RssMessageData> GetFavoriteMessages()
        {
            return GetAllMessagesInner().Where(w => w.IsFavorite).ToList().Select(_mapper.Transform);
        }

        private IQueryable<RssMessageModel> GetAllMessagesInner()
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
        
        public IEnumerable<RssMessageData> GetAllFilterMessages(AllMessageFilterConfiguration filterConfiguration)
        {
            var messages = GetAllMessagesInner();

            messages = filterConfiguration.ApplyFilter(messages);
            messages = filterConfiguration.ApplyDateFilter(messages);

            return messages.ToList().Select(_mapper.Transform);
        }
    }
}
