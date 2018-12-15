using System;
using System.Threading.Tasks;
using Database;
using Database.Rss;
using Realms;
using Shared.App.Rss;
using Shared.App.RssClient;

namespace iOS.App.Rss.RssUpdater
{
	public class RssUpdater
	{
		private static RssUpdater _instance;
		public static RssUpdater Instance => _instance ?? (_instance = new RssUpdater());

		private readonly object _locker = new object();
		private readonly RssApiClient _client;
		private readonly RssRepository _repository;

		private RssUpdater()
		{
			_client = RssApiClient.Instance;
			_repository = RssRepository.Instance;

            Init();
		}

        private async void Init()
        {
            var items = _repository.GetList();

            foreach (var rssModel in items)
            {
                //if (!rssModel.UpdateTime.HasValue || (rssModel.UpdateTime.Value.Date - DateTime.Now).TotalMinutes > 5)
                //{
                    await StartUpdateAllByInternet(rssModel);
                //}
            }

            items.SubscribeForNotifications(async (sender, changes, error) =>
            {
                //await StartUpdateAllByInternet()
            });
        }


		public async Task StartUpdateAllByInternet(RssModel rssModel)
		{
            var request = await _client.Update(rssModel);
            await _repository.Update(rssModel.Id, request);
        }
	}
}