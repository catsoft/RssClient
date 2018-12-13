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

		private readonly IDatabase _localDatabase;

		public RssMessagesRepository()
		{
			_localDatabase = RealmDatabase.Instance;
		}

		public void AddItem(SyndicationItem syndicationItem, RssModel rssModel)
		{
			var imageUri = syndicationItem.Links.FirstOrDefault(w =>
					w.RelationshipType?.Equals("enclosure", StringComparison.InvariantCultureIgnoreCase) == true &&
					w.MediaType?.Equals("image/jpeg", StringComparison.InvariantCultureIgnoreCase) == true)?.Uri
				?.OriginalString;

			var url = syndicationItem.Links.FirstOrDefault(w =>
					w.RelationshipType?.Equals("alternate", StringComparison.InvariantCultureIgnoreCase) == true)?.Uri
				?.OriginalString;

			var item = new RssMessageModel()
			{
				Id = syndicationItem.Id,
				Title = SafeTrim(syndicationItem.Title?.Text),
				Text = SafeTrim(syndicationItem.Summary.Text),
//				CreationDate = syndicationItem.PublishDate.Date,
				Url = url,
				ImageUrl = imageUri,
				Rss = rssModel,
			};

			try
			{
				_localDatabase.Add(item);
			}
			catch (Exception exception)
			{
				// TODO зная что упадет при нахождении такого же элемента можно воспользоваться, а вообще заменить на другое поведение
				// Также зная что много exceptions медленно работают, то точно нужно заменить
			}
		}

		public void Update(RssMessageModel rssMessageModel)
		{
			_localDatabase.AddOrUpdate(rssMessageModel);
		}

		public void DeleteItemsForRss(RssModel rssModel)
		{
			_localDatabase.RemoveRange(rssModel.RssMessageModels.AsQueryable());
		}

		private string SafeTrim(string text)
		{
			return text?.Trim(' ', '\n', '\r');
		}

		public IQueryable<RssMessageModel> GetMessagesForRss(RssModel rssModel)
		{
			var items = _localDatabase.All<RssMessageModel>()
				.Where(w => !w.IsDeleted)
				.OrderByDescending(w => w.CreationDate);

			return items;
		}

		public long GetCountForRss(RssModel rssModel)
		{
			return _localDatabase.All<RssMessageModel>().Where(w => !w.IsDeleted).AsQueryable().Count();
		}
	}
}
