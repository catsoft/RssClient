using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Android.App;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;
using Autofac;
using Droid.Screens.Base.Adapters;
using FFImageLoading;
using Shared;
using Shared.Database.Rss;
using Shared.Repository;
using Shared.Services.Locale;
using Shared.Services.Navigator;
using Shared.ViewModels;
using Xamarin.Essentials;

namespace Droid.Screens.RssItemDetail
{
    public class RssMessageAdapter : WithItemsAdapter<RssMessageModel, List<RssMessageModel>>
    {
        private readonly IRssMessagesRepository _rssMessagesRepository;
        private readonly Color _color;
        private readonly Color _selectableColor;

        public RssMessageAdapter(List<RssMessageModel> items, Activity activity, IRssMessagesRepository rssMessagesRepository) : base(items, activity)
        {
            _rssMessagesRepository = rssMessagesRepository;
            
            _color = new Color(0, 0, 0, 0);
            _selectableColor = new Color(0, 0, 0, 95);
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
                rssMessageViewHolder.Background.SetBackgroundColor(item.IsRead ? _selectableColor : _color);
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

            holder.LeftButtonAction += () => { ReadItem(holder.Item); };
            holder.RightButtonAction += () => { InFavoriteItem(holder.Item); };
            
            return holder;
        }

        private void InFavoriteItem(RssMessageModel holderItem)
        {
            _rssMessagesRepository.ChangeIsFavorite(holderItem);
        }

        private void ReadItem(RssMessageModel holderItem)
        {
            _rssMessagesRepository.ChangeIsRead(holderItem);
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
            var way = App.Container.Resolve<RssMessageViewModel.Way>();
            way.Data = new RssMessageViewModel.Way.WayData(item);
            navigator.Go(way);
        }
    }
}