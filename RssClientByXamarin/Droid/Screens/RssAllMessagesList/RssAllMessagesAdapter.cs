using System;
using System.Globalization;
using System.Linq;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Autofac;
using Droid.NativeExtension;
using Droid.Screens.Base.SwipeButtonRecyclerView;
using FFImageLoading;
using Shared;
using Shared.Database.Rss;
using Shared.Services.Locale;
using Shared.Services.Navigator;
using Shared.ViewModels;

namespace Droid.Screens.RssAllMessagesList
{
    public class RssAllMessagesListAdapter : SwipeButtonListAdapter<RssMessageModel, IQueryable<RssMessageModel>>
    {
        public RssAllMessagesListAdapter(IQueryable<RssMessageModel> items, Activity activity) : base(items, activity)
        {
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var item = Items.ElementAt(position);

            if (holder is RssAllMessagesViewHolder rssMessageViewHolder)
            {
                var localeService = App.Container.Resolve<ILocale>();

                rssMessageViewHolder.Title.Text = item.Title;
                rssMessageViewHolder.Text.Text = item.Text;
                rssMessageViewHolder.CreationDate.Text = item.CreationDate.ToString("d", new CultureInfo(localeService.GetCurrentLocaleId()));
                rssMessageViewHolder.Item = item;
                rssMessageViewHolder.Canal.Text = item.RssLink;

                rssMessageViewHolder.ImageView.Visibility = string.IsNullOrEmpty(item.Url) ? ViewStates.Gone : ViewStates.Visible;

                ImageService.Instance.LoadUrl(item.ImageUrl)
                    .Into(rssMessageViewHolder.ImageView);
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_item_all_rss_message, parent, false);
            var holder = new RssAllMessagesViewHolder(view);

            holder.ClickView.Click += (sender, args) =>
            {
                var navigator = App.Container.Resolve<INavigator>();
                var way = App.Container.Resolve<RssMessageViewModel.Way>();
                way.Data = new RssMessageViewModel.Way.WayData(holder.Item);
                navigator.Go(way);       
            };

            holder.LeftButtonAction += () => { ReadItem(holder.Item); };

            holder.RightButtonAction += () => { InFavoriteItem(holder.Item); };
            
            return holder;
        }

        private void InFavoriteItem(RssMessageModel holderItem)
        {
            Activity.Toast("InFavoriteItem");
        }

        private void ReadItem(RssMessageModel holderItem)
        {
            Activity.Toast("ReadItem");
        }
    }
}