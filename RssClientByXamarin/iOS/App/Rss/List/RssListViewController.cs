using System.Threading;
using System.Threading.Tasks;
using iOS.App.Base.Stated;
using iOS.App.Base.Table;
using Shared.App.Rss;

namespace iOS.App.Rss.List
{
	public class RssListViewController : BaseTableViewController<RssViewCell, RssModel>
	{
		private RssRepository _rssRepository;

		public override async void ViewDidLoad()
		{
			base.ViewDidLoad();

			_rssRepository = RssRepository.Instance;

			_rssRepository.Insert("name1", "name2");
			_rssRepository.Insert("name1", "name2");
			_rssRepository.Insert("name1", "name2");
			_rssRepository.Insert("name1", "name2");
			_rssRepository.Insert("name1", "name2");


			await UpdateData();
		}

		private async Task UpdateData()
		{
			StatedDecorator.SetLoading(new LoadingData());

			var list = await Task.Run(() =>
			{
				Thread.Sleep(2000);
				return _rssRepository.GetList();
			});

			StatedDecorator.SetNormal(new NormalData());

			List.AddRange(list);

			TableView.ReloadData();
		}
	}
}