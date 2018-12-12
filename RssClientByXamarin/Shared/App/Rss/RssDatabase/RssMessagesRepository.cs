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
				if (_localDatabase.GetItems<RssMessageModel>()
					    ?.Any(w => w.CreationDate == syndicationItem.PublishDate.Date) == true)
				{
					// TODO обработать валидацию
					return;
				}

				var item = new RssMessageModel()
				{
					Title = syndicationItem.Title?.Text,
					Text = syndicationItem.Summary.Text,
					CreationDate = syndicationItem.PublishDate.Date,
					Url = syndicationItem.Links?.FirstOrDefault()?.Uri?.AbsoluteUri,
					PrimaryKeyRssModel = rssModel.Id,
				};

				_localDatabase.AddNewItem(item);
			});
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
	}
}
