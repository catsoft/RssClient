using System;
using System.Collections.Generic;
using System.Linq;
using Shared.App.Base.Database;

namespace Shared.App.Rss
{
	public class RssRepository
	{
		private static RssRepository _instance;
		private readonly LocalDb _localDatabase;

		public static RssRepository Instance => _instance ?? (_instance = new RssRepository());

		private RssRepository()
		{
			_localDatabase = LocalDb.Instance;
		}

		public void Insert(string name, string url)
		{
			var newItem = new RssModel(name, url, DateTime.Now);
			_localDatabase.AddNewItem(newItem);
		}

		public void Update(RssModel item)
		{
			_localDatabase.UpdateItemByLocalId(item);
		}

		public void Remove(RssModel item)
		{
			_localDatabase.DeleteItemByLocalId(item);
		}

		public List<RssModel> GetList()
		{
			return _localDatabase.GetItems<RssModel>()?.OrderBy(w => w.CreationTime).ToList();
		}
	}
}
