using System.Collections.Generic;
using Autofac;
using Core;
using Core.Database.Rss;
using Core.Repositories.Rss;
using Core.Repositories.RssMessage;
using iOS.Screens.Base.Stated;
using iOS.Screens.Base.Table;
using iOS.Screens.Edit;
using Realms;
using UIKit;
using Xamarin;

namespace iOS.Screens.Detail
{
    public class RssDetailViewController : BaseTableViewController<RssMessageViewCell, RssMessageModel, IEnumerable<RssMessageModel>>
    {
        private readonly RssModel _item;
        private readonly IRssMessagesRepository _rssMessagesRepository;
        private readonly IRssRepository _repository;

        public RssDetailViewController(RssModel item)
        {
            _item = item;

            _repository = App.Container.Resolve<IRssRepository>();
            _rssMessagesRepository = App.Container.Resolve<IRssMessagesRepository>();
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
                    _repository.Remove(_item.Id);
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
                var newItems = _rssMessagesRepository.GetMessagesForRss(_item.Id);
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