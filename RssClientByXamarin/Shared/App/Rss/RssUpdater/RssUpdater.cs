using System;
using System.Threading.Tasks;
using Database;
using Database.Rss;
using Shared.App.Rss;
using Shared.App.RssClient;
using SQLite;

namespace iOS.App.Rss.RssUpdater
{
	public class RssUpdater
	{
		private static RssUpdater _instance;
		public static RssUpdater Instance => _instance ?? (_instance = new RssUpdater());

		private readonly object _locker = new object();
		private bool _isUpdateting;
		private readonly RssApiClient _client;
		private readonly RssRepository _repository;
		private readonly Database.ILocalDb _localDb;

		public event Action<RssModel> UpdateModel;

		private RssUpdater()
		{
			_client = RssApiClient.Instance;
			_repository = RssRepository.Instance;
			_localDb = LocalDb.Instance;

			_localDb.TableChanges += TableChanges;
		}

		public async Task StartUpdateByInternet(RssModel item)
		{
			lock (_locker)
			{
				if(_isUpdateting)
					return;

				SetLockedFlag(true);
			}

			var request = await _client.Update(item);
			await _repository.Update(item, request);

			SetLockedFlag(false);

			UpdateModel?.Invoke(item);
		}

		public async Task StartUpdateAllByInternet()
		{
			lock (_locker)
			{
				if (_isUpdateting)
					return;

				SetLockedFlag(true);
			}

			var items = await _repository.GetList();
			foreach (var rssModel in items)
			{
				var request = await _client.Update(rssModel);
				await _repository.Update(rssModel, request);

				UpdateModel?.Invoke(rssModel);
			}

			SetLockedFlag(false);
		}

		private void SetLockedFlag(bool value)
		{
			lock (_locker)
			{
				_isUpdateting = value;
			}
		}

		private void TableChanges(object sender, NotifyTableChangedEventArgs e)
		{
			CollectionChanged?.Invoke(sender, e);
		}

		public EventHandler<NotifyTableChangedEventArgs> CollectionChanged { get; set; }
	}
}