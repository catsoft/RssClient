using System.Collections.Generic;
using Database.Rss;
using iOS.App.Base.Stated;
using iOS.App.Base.Table;
using iOS.App.RssScreens.Edit;
using Realms;
using Shared.App.Rss.RssDatabase;
using Xamarin;

namespace iOS.App.RssScreens.Detail
{
	public class RssDetailViewController : BaseTableViewController<RssMessageViewCell, RssMessageModel, IEnumerable<RssMessageModel>>
	{
		private readonly RssModel _item;
		private readonly RssMessagesRepository _rssMessagesRepository;

		public RssDetailViewController(RssModel item)
		{
			_item = item;

			_rssMessagesRepository = RssMessagesRepository.Instance;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			if (NavigationItem != null)
			{
				NavigationItem.Title = _item.Name;

				var editButton = new IQBarButtonItem();
				editButton.Title = "Edit";
				editButton.Clicked += (sender, args) =>
				{
					NavigationController?.PushViewController(new RssEditViewController(_item), true);
				};

				NavigationItem.RightBarButtonItem = editButton;
			}

			var items = _rssMessagesRepository.GetMessagesForRss(_item);

			Source.SetList(items);

            DataSetChanges();

            _item.PropertyChanged += (sender, args) =>
            {
                var newItems = _rssMessagesRepository.GetMessagesForRss(_item);
                Source.SetList(newItems);

                DataSetChanges();
            };

            _item.RssMessageModels.SubscribeForNotifications((sender, changes, error) =>
            {
                var newItems = _rssMessagesRepository.GetMessagesForRss(_item);
                Source.SetList(newItems);

                DataSetChanges();
            });
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