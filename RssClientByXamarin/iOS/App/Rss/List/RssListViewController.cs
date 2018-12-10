using System.Threading.Tasks;
using iOS.App.Base.Stated;
using iOS.App.Base.Table;
using iOS.App.Styles;
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

			if (NavigationItem != null)
			{
				NavigationItem.Title = Strings.RssListTitle;
			}

			await _rssRepository.Insert("name2", "name2");

			await UpdateData();
		}

		private async Task UpdateData()
		{
			StatedDecorator.SetLoading(new LoadingData());

			var list = await _rssRepository.GetList();

			StatedDecorator.SetNormal(new NormalData());

			List.AddRange(list);

			TableView.ReloadData();
		}
	}
}