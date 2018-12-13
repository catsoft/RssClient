using System.Threading.Tasks;
using Database.Rss;
using iOS.App.Base.Stated;
using iOS.App.Base.Table;
using iOS.App.Rss.Create;
using iOS.App.Rss.Detail;
using iOS.App.Styles;
using Shared.App.Rss;
using UIKit;
using Xamarin;

namespace iOS.App.Rss.List
{
	public class RssListViewController : BaseTableViewController<RssViewCell, RssModel>
	{
		private readonly RssRepository _rssRepository;
		private readonly RssUpdater.RssUpdater _rssUpdater;

		public RssListViewController()
		{
			_rssRepository = RssRepository.Instance;
			_rssUpdater = RssUpdater.RssUpdater.Instance;
		}

		public override async void ViewDidLoad()
		{
			base.ViewDidLoad();

			if (NavigationItem != null)
			{
				NavigationItem.Title = Strings.RssListTitle;
				var rightItem = new IQBarButtonItem()
				{
					Image = UIImage.FromBundle("Plus")
				};
				NavigationItem.RightBarButtonItem = rightItem;
				rightItem.Clicked += (sender, args) =>
				{
					NavigationController?.PushViewController(new RssCreateViewController(), true);
				};
			}

			Source.ItemSelected += model =>
			{
				NavigationController?.PushViewController(new RssDetailViewController(model), true);
			};

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