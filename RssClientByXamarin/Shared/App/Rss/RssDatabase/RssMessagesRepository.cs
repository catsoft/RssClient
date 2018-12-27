using System.Collections.Generic;
using System.Linq;
using Analytics.Rss;
using Database;
using Database.Rss;

namespace Shared.App.Rss.RssDatabase
{
	public class RssMessagesRepository
	{
		private static RssMessagesRepository _instance;
		public static RssMessagesRepository Instance => _instance ?? (_instance = new RssMessagesRepository());

		private readonly RealmDatabase _localDatabase;

		public RssMessagesRepository()
		{
			_localDatabase = RealmDatabase.Instance;
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
