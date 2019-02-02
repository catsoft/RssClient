using System.Collections.Generic;
using System.Linq;
using Shared.Database;
using Shared.Database.Rss;

namespace Shared.Repository
{
    public class RssMessagesRepository : IRssMessagesRepository
    {
        private readonly RealmDatabase _localDatabase;

        public RssMessagesRepository(RealmDatabase localDatabase)
        {
            _localDatabase = localDatabase;
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
            return rssModel.RssMessageModels.ToList();
        }

        public long GetCountForModel(RssModel rssModel)
        {
            return rssModel.RssMessageModels.Count();
        }

        public IQueryable<RssMessageModel> GetAllMessages()
        {
            return _localDatabase.MainThreadRealm.All<RssMessageModel>().OrderByDescending(w => w.CreationDate);
        }
    }
}
