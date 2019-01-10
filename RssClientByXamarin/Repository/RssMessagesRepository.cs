using System.Collections.Generic;
using System.Linq;
using Database;
using Database.Rss;

namespace Repository
{
	public class RssMessagesRepository : IRssMessagesRepository
    {
		private readonly RealmDatabase _localDatabase;

		public RssMessagesRepository(RealmDatabase localDatabase)
        {
            _localDatabase = localDatabase;
        }

        public void MarkAsDeleted(RssMessageModel rssMessageModel)
        {
            _localDatabase.UpdateInBackground<RssMessageModel>(rssMessageModel.Id, message => { message.IsDeleted = true; });
        }

        public void MarkAsRead(RssMessageModel rssMessageModel)
        {
            _localDatabase.UpdateInBackground<RssMessageModel>(rssMessageModel.Id, message => { message.IsRead = true; });
        }

		public IEnumerable<RssMessageModel> GetMessagesForRss(RssModel rssModel)
		{
			return rssModel.RssMessageModels.ToList().Where(w => !w.IsDeleted);
		}

        public long GetCountForModel(RssModel rssModel)
        {
            return rssModel.RssMessageModels.Count(w => !w.IsDeleted);
        }
    }
}
