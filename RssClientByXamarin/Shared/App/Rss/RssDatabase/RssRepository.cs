using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using Database;
using Database.Rss;

namespace Shared.App.Rss
{
	public class RssRepository
	{
		private static RssRepository _instance;
		public static RssRepository Instance => _instance ?? (_instance = new RssRepository());

		private readonly LocalDb _localDatabase;

		private RssRepository()
		{
			_localDatabase = LocalDb.Instance;
		}

		public Task Insert(string name, string url)
		{
			return Task.Run(() =>
			{
				var newItem = new RssModel()
				{
					Rss = url,
					Name = name,
					CreationTime = DateTime.Now,
				};
				_localDatabase.AddNewItem(newItem); 
			});
		}
		//public RssMessageModel(SyndicationItem syndicationItem, int primaryKey)
		//{
		//	Title = syndicationItem.Title?.Text;
		//	Text = syndicationItem.Summary.Text;
		//	CreationDate = syndicationItem.PublishDate.Date;
		//	Url = syndicationItem.Links?.FirstOrDefault()?.Uri?.AbsoluteUri;

		//	PrimaryKeyRssModel = primaryKey;
		//}

		public Task Update(RssModel item)
		{
			return Task.Run(() => _localDatabase.UpdateItemByLocalId(item));
		}

		public Task Remove(RssModel item)
		{
			return Task.Run(() => _localDatabase.DeleteItemByLocalId(item));
		}

		public Task<List<RssModel>> GetList()
		{
			return Task.Run(() => _localDatabase.GetItems<RssModel>()?.OrderBy(w => w.CreationTime).ToList());
		}

		public Task Update(RssModel item, SyndicationFeed feed)
		{
			return Task.Run(() =>
			{
				if (feed == null)
					return;

				item.Name = feed.Title?.Text;
				item.UpdateTime = DateTime.Now;
				item.UrlPreviewImage = feed.Links?.FirstOrDefault()?.Uri?.OriginalString + "/favicon.ico";

				_localDatabase.UpdateItemByLocalId(item);
			});
		}
	}
}
