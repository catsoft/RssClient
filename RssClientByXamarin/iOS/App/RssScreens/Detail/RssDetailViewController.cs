using System.Collections.Generic;
using Database.Rss;
using iOS.App.Base.Stated;
using iOS.App.Base.Table;
using iOS.App.RssScreens.Edit;
using Realms;
using Shared.App.Rss;
using Shared.App.Rss.RssDatabase;
using UIKit;
using Xamarin;

namespace iOS.App.RssScreens.Detail
{
    public class RssDetailViewController : BaseTableViewController<RssMessageViewCell, RssMessageModel, IEnumerable<RssMessageModel>>
    {
        private readonly RssModel _item;
        private readonly RssMessagesRepository _rssMessagesRepository;
        private readonly RssRepository _repository;

        public RssDetailViewController(RssModel item)
        {
            _item = item;

            _rssMessagesRepository = RssMessagesRepository.Instance;
            _repository = RssRepository.Instance;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            if (NavigationItem != null)
            {
                NavigationItem.Title = _item.Name;

                var editButton = new IQBarButtonItem {Title = "Edit"};
                editButton.Clicked += (sender, args) => { NavigationController?.PushViewController(new RssEditViewController(_item), true); };

                var deleteButton = new IQBarButtonItem {Title = "Delete"};
                deleteButton.Clicked += (sender, args) =>
                {
                    _repository.Remove(_item);
                    NavigationController?.PopViewController(true);
                };

                NavigationItem.RightBarButtonItems = new UIBarButtonItem[] {editButton, deleteButton};
            }

            RefresherValueChanged += async () =>
            {
                var id = _item.Id;
                var url = _item.Rss;
                await _repository.StartUpdateAllByInternet(url, id);

                RefreshControl.EndRefreshing();
            };

            ReloadData();

            _item.PropertyChanged += (sender, args) => ReloadData();
            _item.RssMessageModels.SubscribeForNotifications((sender, changes, error) => ReloadData());
        }

        private void ReloadData()
        {
            if (_item.IsValid)
            {
                var newItems = _rssMessagesRepository.GetMessagesForRss(_item);
                Source.SetList(newItems);

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
}