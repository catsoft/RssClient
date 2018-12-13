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

		public Task AddItem(SyndicationItem syndicationItem, RssModel rssModel)
		{
			return Task.Run(() =>
			{
				var imageUri = syndicationItem.Links.FirstOrDefault(w =>
						w.RelationshipType?.Equals("enclosure", StringComparison.InvariantCultureIgnoreCase) == true &&
						w.MediaType?.Equals("image/jpeg", StringComparison.InvariantCultureIgnoreCase) == true)?.Uri?.OriginalString;

				var url = syndicationItem.Links.FirstOrDefault(w =>
					w.RelationshipType?.Equals("alternate", StringComparison.InvariantCultureIgnoreCase) == true)?.Uri?.OriginalString;

				var item = new RssMessageModel()
				{
					Id = syndicationItem.Id,
					Title = SafeTrim(syndicationItem.Title?.Text),
					Text = SafeTrim(syndicationItem.Summary.Text),
					CreationDate = syndicationItem.PublishDate.Date,
					Url = url,
					ImageUrl = imageUri,
					PrimaryKeyRssModel = rssModel.Id,
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
			});
		}

		public Task Update(RssMessageModel rssMessageModel)
		{
			return Task.Run(() =>
			{
				_localDatabase.AddOrUpdate(rssMessageModel);
			});
		}

		public Task DeleteItemsForRss(RssModel rssModel)
		{
			return Task.Run(() =>
			{
				var items = _localDatabase.All<RssMessageModel>()?.Where(w => w.PrimaryKeyRssModel == rssModel.Id);
				_localDatabase.RemoveRange(items);
			});
		}

		private string SafeTrim(string text)
		{
			return text?.Trim(' ', '\n', '\r');
		}

		public Task<IQueryable<RssMessageModel>> GetMessagesForRss(RssModel rssModel)
		{
			return Task.Run<IQueryable<RssMessageModel>>(() =>
			{
				return _localDatabase.All<RssMessageModel>()
					?.Where(w => !w.IsDeleted && w.PrimaryKeyRssModel == rssModel.Id)
					.OrderByDescending(w => w.CreationDate);
			});
		}

		public Task<long> GetCountForRss(RssModel rssModel)
		{
			return Task.Run<long>(() =>
			{
				return _localDatabase.All<RssMessageModel>()?.Where(w => !w.IsDeleted && w.PrimaryKeyRssModel == rssModel.Id).Count() ?? 0;
			});
		}
	}
}
