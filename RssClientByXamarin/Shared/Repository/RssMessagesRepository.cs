using System.Collections.Generic;
using System.Linq;
using Droid.Repository;
using Shared.Configuration;
using Shared.Database;
using Shared.Database.Rss;

namespace Shared.Repository
{
    public class RssMessagesRepository : IRssMessagesRepository
    {
        private readonly RealmDatabase _localDatabase;
        private readonly IConfigurationRepository _configurationRepository;

        public RssMessagesRepository(RealmDatabase localDatabase, IConfigurationRepository configurationRepository)
        {
            _localDatabase = localDatabase;
            _configurationRepository = configurationRepository;
        }

        public RssMessageModel FindById(string id)
        {
            return _localDatabase.MainThreadRealm.Find<RssMessageModel>(id);
        }

        public void MarkAsFavorite(RssMessageModel rssMessageModel)
        {
            RealmDatabase.UpdateInBackground<RssMessageModel>(rssMessageModel.Id,(message, realm) => { message.IsFavorite = true; });
        }

        public void MarkAsRead(RssMessageModel rssMessageModel)
        {
            RealmDatabase.UpdateInBackground<RssMessageModel>(rssMessageModel.Id, (message, realm) => { message.IsRead = true; });
        }

        public void ChangeIsFavorite(RssMessageModel rssMessageModel)
        {
            RealmDatabase.UpdateInBackground<RssMessageModel>(rssMessageModel.Id,(message, realm) => { message.IsFavorite = !message.IsFavorite; });
        }

        public void ChangeIsRead(RssMessageModel rssMessageModel)
        {
            RealmDatabase.UpdateInBackground<RssMessageModel>(rssMessageModel.Id, (message, realm) => { message.IsRead = !message.IsRead; });
        }

        public IEnumerable<RssMessageModel> GetMessagesForRss(RssModel rssModel)
        {
            var appConfiguration = _configurationRepository.GetSettings<AppConfiguration>();
            var hideReadMessages = appConfiguration.HideReadMessages;
            IEnumerable<RssMessageModel> messages = rssModel.RssMessageModels;
            if (hideReadMessages)
                messages = messages.Where(w => !w.IsRead);
            return messages.OrderBy(w => w.CreationDate);
        }

        public long GetCountNewMessagesForModel(RssModel rssModel)
        {
            return rssModel.RssMessageModels.Count(w => !w.IsRead);
        }
        
        {
            return rssModel.RssMessageModels.Count(w => !w.IsRead);
        }

        public IQueryable<RssMessageModel> GetAllMessages()
        {
            var appConfiguration = _configurationRepository.GetSettings<AppConfiguration>();
            var hideReadMessages = appConfiguration.HideReadMessages;
            IQueryable<RssMessageModel> messages = _localDatabase.MainThreadRealm.All<RssMessageModel>()
                .OrderByDescending(w => w.CreationDate);

            if (hideReadMessages)
                messages = messages.Where(w => !w.IsRead);

            return messages;
        }
    }
}
