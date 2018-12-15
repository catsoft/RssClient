using System;
using System.Linq;
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
                if (!rssModel.UpdateTime.HasValue || (rssModel.UpdateTime.Value.Date - DateTime.Now).TotalMinutes > 5)
                {
                    await StartUpdateAllByInternet(rssModel);
                }
            }

            items.SubscribeForNotifications(async (sender, changes, error) =>
            {
                if (sender != null && changes != null)
                {
                    foreach (var changesInsertedIndex in changes.InsertedIndices)
                    {
                        var item = sender.ElementAt(changesInsertedIndex);
                        await StartUpdateAllByInternet(item);
                    }

                    foreach (var changesInsertedIndex in changes.ModifiedIndices)
                    {
                        var item = sender.ElementAt(changesInsertedIndex);
                        if (!item.UpdateTime.HasValue || (item.UpdateTime.Value.Date - DateTime.Now).TotalMinutes > 5)
                        {
                            await StartUpdateAllByInternet(item);
                        }
                    }
                }
            });
        }


		public async Task StartUpdateAllByInternet(RssModel rssModel)
		{
            var request = await _client.Update(rssModel);
            if(request != null)
                await _repository.Update(rssModel.Id, request);
        }
	}
}