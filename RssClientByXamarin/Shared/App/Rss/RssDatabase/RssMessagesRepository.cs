﻿using System;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using Database;
using Database.Rss;
using Realms;

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

		public Task AddItem(SyndicationItem syndicationItem, string rssModelId)
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

                    realm.Write(() =>
                    {
                        try
                        {
                            currentItem.RssMessageModels.Add(item);
                        }
                        catch (Exception e)
                        {
                            // TODO зная что упадет при нахождении такого же элемента можно воспользоваться, а вообще заменить на другое поведение
                            // Также зная что много exceptions медленно работают, то точно нужно заменить
                        }
                    });
                }
            });
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

		public IQueryable<RssMessageModel> GetMessagesForRss(RssModel rssModel)
		{
//			return _localDatabase.Realm.All<RssMessageModel>().Where(w => w.Rss == rssModel && !w.IsDeleted);

			return rssModel.RssMessageModels.AsRealmCollection().AsQueryable();

//			var items = rssModel.RssMessageModels.AsQueryable()
//				.Where(w => w != null && !w.IsDeleted)
//				.OrderByDescending(w => w.CreationDate);
//
//			return items;
		}
	}
}
