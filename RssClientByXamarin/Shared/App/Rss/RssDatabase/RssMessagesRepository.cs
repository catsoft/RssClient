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

		private readonly LocalDb _localDatabase;

		public RssMessagesRepository()
		{
			_localDatabase = LocalDb.Instance;
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

				_localDatabase.AddNewItem(item);
			});
		}

		private string SafeTrim(string text)
		{
			return text?.Trim(' ', '\n', '\r');
		}

		public Task<List<RssMessageModel>> GetMessagesForRss(RssModel rssModel)
		{
			return Task.Run(() =>
			{
				return _localDatabase.GetItems<RssMessageModel>()?.Where(w => w.PrimaryKeyRssModel == rssModel.Id)
					.OrderBy(w => w.CreationDate)
					.ToList();
			});
		}

		public Task<long> GetCountForRss(RssModel rssModel)
		{
			return Task.Run<long>(() =>
			{
				return _localDatabase.GetItems<RssMessageModel>()?.Where(w => w.PrimaryKeyRssModel == rssModel.Id).Count() ?? 0;
			});
		}
	}
}
