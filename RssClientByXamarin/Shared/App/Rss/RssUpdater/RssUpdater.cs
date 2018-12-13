using System.Threading.Tasks;
using Database;
using Database.Rss;
using Shared.App.Rss;
using Shared.App.RssClient;

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

		private RssUpdater()
		{
			_client = RssApiClient.Instance;
			_repository = RssRepository.Instance;
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
			_repository.Update(item, request);

			SetLockedFlag(false);
		}

		public async Task StartUpdateAllByInternet()
		{
			lock (_locker)
			{
				if (_isUpdateting)
					return;

				SetLockedFlag(true);
			}

			var items = _repository.GetList();
			foreach (var rssModel in items)
			{
				var request = await _client.Update(rssModel);
				_repository.Update(rssModel, request);
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
	}
}