using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

		public Task Insert(string name, string url)
		{
			return Task.Run(() =>
			{
				var newItem = new RssModel(name, url, DateTime.Now);
				_localDatabase.AddNewItem(newItem); 
			});
		}

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
	}
}
