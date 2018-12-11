using System.Threading.Tasks;
using iOS.App.Base.Stated;
using iOS.App.Base.Table;
using iOS.App.Styles;
using Shared.App.Rss;
using UIKit;
using Xamarin;

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
				NavigationItem.RightBarButtonItem = new IQBarButtonItem()
				{
//					Image = UIImage.FromBundle()
				};
			}

			await _rssRepository.Insert("name2", "name2");
		}

		public override async void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			await UpdateData();
		}

		private async Task UpdateData()
		{
			StatedDecorator.SetLoading(new LoadingData());

			var list = await _rssRepository.GetList();

			StatedDecorator.SetNormal(new NormalData());

			List.Clear();
			List.AddRange(list);

			TableView.ReloadData();
		}
	}
}