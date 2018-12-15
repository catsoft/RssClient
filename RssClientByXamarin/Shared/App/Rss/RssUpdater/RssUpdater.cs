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

            if (!items.Any())
            {
                _repository.InsertByUrl("https://meteoinfo.ru/rss/forecasts/index.php?s=28440");
                _repository.InsertByUrl("https://acomics.ru/~depth-of-delusion/rss");
                _repository.InsertByUrl("http://www.calend.ru/img/export/calend.rss");
                _repository.InsertByUrl("http://www.old-hard.ru/rss");
                _repository.InsertByUrl("https://lenta.ru/rss/news");
                _repository.InsertByUrl("https://lenta.ru/rss/articles");
                _repository.InsertByUrl("https://lenta.ru/rss/top7");
                _repository.InsertByUrl("https://lenta.ru/rss/news/russia");
            }

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