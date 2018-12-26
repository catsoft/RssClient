using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
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

		public Task AddOrUpdateItem(SyndicationItem syndicationItem, string rssModelId)
        {
            return Task.Run(() =>
            {
                var imageUri = syndicationItem.Links.FirstOrDefault(w =>
                        w.RelationshipType?.Equals("enclosure", StringComparison.InvariantCultureIgnoreCase) == true &&
                        w.MediaType?.Equals("image/jpeg", StringComparison.InvariantCultureIgnoreCase) == true)?.Uri
                    ?.OriginalString;

                var url = syndicationItem.Links.FirstOrDefault(w =>
                        w.RelationshipType?.Equals("alternate", StringComparison.InvariantCultureIgnoreCase) == true)
                    ?.Uri
                    ?.OriginalString;

                var item = new RssMessageModel()
                {
                    SyndicationId = syndicationItem.Id,
                    Title = SafeTrim(syndicationItem.Title?.Text),
                    Text = SafeTrim(syndicationItem.Summary.Text),
                    //				CreationDate = syndicationItem.PublishDate.Date,
                    Url = url,
                    ImageUrl = imageUri,
                };

                using (var realm = RealmDatabase.Instance.OpenDatabase)
                {
                    var currentItem = realm.Find<RssModel>(rssModelId);
                    var rssMessage = currentItem.RssMessageModels.FirstOrDefault(w => w.SyndicationId == item.SyndicationId);

                    if (rssMessage != null)
                        item.Id = rssMessage.Id;

                    using (var transaction = realm.BeginWrite())
                    {
                        if (rssMessage != null)
                        {
                            realm.Add(rssMessage, true);
                        }
                        else
                        {
                            currentItem.RssMessageModels.Add(item);
                        }
                        transaction.Commit();
                    }
                }
            });
        }

        public void MarkAsDeleted(RssMessageModel rssMessageModel)
        {
            _localDatabase.DoInBackground(rssMessageModel, message => { message.IsDeleted = true; });
        }

        public void MarkAsRead(RssMessageModel rssMessageModel)
        {
            _localDatabase.DoInBackground(rssMessageModel, message => { message.IsRead = true; });
        }

        public void Update(RssMessageModel rssMessageModel)
		{
			_localDatabase.MainThreadRealm.Write(() =>
			{
				_localDatabase.MainThreadRealm.Add(rssMessageModel, true);
			});
		}

		private string SafeTrim(string text)
		{
			return text?.Trim(' ', '\n', '\r');
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
