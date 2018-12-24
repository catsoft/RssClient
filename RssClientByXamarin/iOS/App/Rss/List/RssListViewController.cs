using System.Linq;
using Database.Rss;
using iOS.App.Base.Stated;
using iOS.App.Base.Table;
using iOS.App.Rss.Create;
using iOS.App.Rss.Detail;
using iOS.App.Styles;
using Realms;
using Shared.App.Rss;
using UIKit;
using Xamarin;

namespace iOS.App.Rss.List
{
	public class RssListViewController : BaseTableViewController<RssViewCell, RssModel>
	{
		private readonly RssRepository _rssRepository;

		public RssListViewController()
		{
			_rssRepository = RssRepository.Instance;
		}

		public override void ViewDidLoad()
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

			var list = _rssRepository.GetList();
			Source.SetList(list);

			TableView.ReloadData();

			if (list.Any())
			{
				StatedDecorator.SetNormal(new NormalData());
			}
			else
			{
				StatedDecorator.SetError(new ErrorData());
			}

			list.SubscribeForNotifications((sender, changes, error) =>
			{
				TableView.ReloadData();
			});
		}
	}
}