using System.Linq;
using Autofac;
using Core;
using Core.Database.Rss;
using Core.Repositories.Rss;
using iOS.Screens.Base.Stated;
using iOS.Screens.Base.Table;
using iOS.Screens.Create;
using iOS.Screens.Detail;
using iOS.Styles;
using UIKit;
using Xamarin;

namespace iOS.Screens.List
{
	public class RssListViewController : BaseTableViewController<RssViewCell, RssModel, IQueryable<RssModel>>
	{
		private readonly IRssRepository _rssRepository;

		public RssListViewController()
		{
			_rssRepository = App.Container.Resolve<IRssRepository>();
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

            DataSetChanges();

			list.SubscribeForNotifications((sender, changes, error) => DataSetChanges());
		}

        private void DataSetChanges()
        {
            TableView.ReloadData();

            if (Source.ItemsCount > 0)
            {
                StatedDecorator.SetNormal(new NormalData());
            }
            else
            {
                StatedDecorator.SetError(new ErrorData());
            }
        }
    }
}