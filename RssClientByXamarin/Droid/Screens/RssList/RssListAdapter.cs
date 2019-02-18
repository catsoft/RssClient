using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Autofac;
using Droid.Screens.Base.Adapters;
using Droid.Screens.Base.SwipeRecyclerView;
using Shared;
using Shared.Configuration.Settings;
using Shared.Database.Rss;
using Shared.Repository.Rss;
using Shared.Repository.RssMessage;
using Shared.Services.Locale;
using Shared.Services.Navigator;
using Shared.ViewModels;
using Xamarin.Essentials;

namespace Droid.Screens.RssList
{
    public class RssListAdapter : DataBindAdapter<RssData, IEnumerable<RssData>, RssListViewHolder>, IItemTouchHelperAdapter
    {
        private readonly IRssRepository _rssRepository;
        private readonly IRssMessagesRepository _rssMessagesRepository;
        private readonly INavigator _navigator;
        private readonly AppConfiguration _appConfiguration;

        public void OnItemDismiss(int position)
        {
            var item = Items.ElementAt(position);
            _rssRepository.Remove(item.Id);
            NotifyItemRemoved(position);
        }

        public RssListAdapter(IEnumerable<RssData> items, Activity activity, AppConfiguration appConfiguration) : base(items, activity)
        {
            _appConfiguration = appConfiguration;
            _rssRepository = App.Container.Resolve<IRssRepository>();
            _rssMessagesRepository = App.Container.Resolve<IRssMessagesRepository>();
            _navigator = App.Container.Resolve<INavigator>();
        }

        protected override void BindData(RssListViewHolder holder, RssData item)
        {
            base.BindData(holder, item);

            holder.SubtitleTextView.Text = item.UpdateTime == null
                ? Activity.GetText(Resource.String.rssList_notUpdated)
                : $"{Activity.GetText(Resource.String.rssList_updated)} {item.UpdateTime.Value.ToShortGeneralLocaleString()}";
            holder.CountTextView.Text = _rssMessagesRepository.GetCountNewMessagesForModel(item.Id).ToString();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_item_rss, parent, false);
            var holder = new RssListViewHolder(view, _appConfiguration.LoadAndShowImages);
            holder.ClickView.Click += (sender, args) => { OpenDetailActivity(holder.Item); };
            holder.ClickView.LongClick += (sender, args) => { ItemLongClick(holder.Item, sender); };

            return holder;
        }

        private void ItemLongClick(RssData holderItem, object sender)
        {
            var menu = new PopupMenu(Activity, sender as View, (int) GravityFlags.Right);
            menu.MenuItemClick += (o, eventArgs) => MenuClick(holderItem, eventArgs);
            menu.Inflate(Resource.Menu.contextMenu_rssList);
            menu.Show();
        }

        private void MenuClick(RssData holderItem, PopupMenu.MenuItemClickEventArgs eventArgs)
        {
            switch (eventArgs.Item.ItemId)
            {
                case Resource.Id.menuItem_rssList_contextEdit:
                    EditItem(holderItem);
                    break;
                case Resource.Id.menuItem_rssList_contextRemove:
                    DeleteItem(holderItem);
                    break;
                case Resource.Id.menuItem_rssList_contextShare:
                    ShareItem(holderItem);
                    break;
                case Resource.Id.menuItem_rssList_contextReadAllMessages:
                    ReadAll(holderItem);
                    break;
            }
        }

        private void ReadAll(RssData holderItem)
        {
            _rssRepository.ReadAllMessages(holderItem.Id);
        }

        private async void ShareItem(RssData holderItem)
        {
            await Share.RequestAsync(holderItem.Rss);
        }

        private void EditItem(RssData holderItem)
        {
            var navigator = App.Container.Resolve<INavigator>();
            var editWay = App.Container.Resolve<RssEditViewModel.Way>();
            editWay.Data = new RssEditViewModel.Way.WayData(holderItem.Id);
            navigator.Go(editWay);
        }

        private void DeleteItem(RssData holderItem)
        {
            var builder = new AlertDialog.Builder(Activity);
            builder.SetPositiveButton(Activity.GetText(Resource.String.rssDeleteDialog_positiveTitle),
                (sender, args) => { _rssRepository.Remove(holderItem.Id); });
            builder.SetNegativeButton(Activity.GetText(Resource.String.rssDeleteDialog_negativeTitle),
                (sender, args) => { });
            builder.SetTitle(Activity.GetText(Resource.String.rssDeleteDialog_Title));
            builder.Show();
        }

        private void OpenDetailActivity(RssData holderItem)
        {
            var way = App.Container.Resolve<RssItemDetailViewModel.Way>();
            way.Data = new RssItemDetailViewModel.Way.WayData(holderItem);
            _navigator.Go(way);
        }
    }
}