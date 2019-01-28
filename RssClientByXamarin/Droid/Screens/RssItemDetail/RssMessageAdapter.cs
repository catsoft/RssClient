using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Autofac;
using Droid.Screens.Base.Adapters;
using FFImageLoading;
using Shared;
using Shared.Database.Rss;
using Shared.Services.Locale;
using Shared.Services.Navigator;
using Shared.ViewModels;
using Xamarin.Essentials;

namespace Droid.Screens.RssItemDetail
{
	public class RssMessageAdapter : WithItemsAdapter<RssMessageModel, List<RssMessageModel>>
    {
		public RssMessageAdapter(List<RssMessageModel> items, Activity activity) : base(items, activity)
        {
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var item = Items.ElementAt(position);

            if (holder is RssMessageViewHolder rssMessageViewHolder)
            {
                var localeService = App.Container.Resolve<ILocale>();

                rssMessageViewHolder.Title.Text = item.Title;
                rssMessageViewHolder.Text.Text = item.Text;
                rssMessageViewHolder.CreationDate.Text = item.CreationDate.ToString("d", new CultureInfo(localeService.GetCurrentLocaleId()));
                rssMessageViewHolder.Item = item;

                rssMessageViewHolder.ImageView.Visibility = string.IsNullOrEmpty(item.Url) ? ViewStates.Gone : ViewStates.Visible;

                ImageService.Instance.LoadUrl(item.ImageUrl).Into(rssMessageViewHolder.ImageView);
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_item_rss_message, parent, false);
            var holder = new RssMessageViewHolder(view);

            holder.ClickView.Click += (sender, args) => { OpenContentActivity(holder.Item); };
            holder.ClickView.LongClick += (sender, args) => { ItemLongClick(holder.Item, sender); };
            
            return holder;
        }

        private void ItemLongClick(RssMessageModel holderItem, object sender)
        {
            var menu = new PopupMenu(Activity, sender as View, (int) GravityFlags.Right);
            menu.MenuItemClick += (o, eventArgs) => MenuClick(holderItem, eventArgs);
            menu.Inflate(Resource.Menu.contextMenu_rssDetailList);
            menu.Show();
        }

        private void MenuClick(RssMessageModel holderItem, PopupMenu.MenuItemClickEventArgs eventArgs)
        {
            switch (eventArgs.Item.ItemId)
            {
                case Resource.Id.menuItem_rssDetailList_contextShare:
                    ShareItem(holderItem);
                    break;
            }   
        }

        private async void ShareItem(RssMessageModel holderItem)
        {
            await Share.RequestAsync(holderItem.Url);
        }

        private void OpenContentActivity(RssMessageModel item)
        {
            var navigator = App.Container.Resolve<INavigator>();
            var way = App.Container.Resolve<IWay<RssMessageViewModel, RssMessageViewModel.Way.WayData>>();
            way.Data = new RssMessageViewModel.Way.WayData(item);
            navigator.Go(way);
        }
    }
}