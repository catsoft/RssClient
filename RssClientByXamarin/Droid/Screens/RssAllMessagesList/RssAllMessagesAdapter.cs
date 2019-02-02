using System;
using System.Globalization;
using System.Linq;
using Android.App;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;
using Autofac;
using Droid.NativeExtension;
using Droid.Screens.Base.SwipeButtonRecyclerView;
using FFImageLoading;
using Shared;
using Shared.Database.Rss;
using Shared.Repository;
using Shared.Services.Locale;
using Shared.Services.Navigator;
using Shared.ViewModels;

namespace Droid.Screens.RssAllMessagesList
{
    public class RssAllMessagesListAdapter : SwipeButtonListAdapter<RssMessageModel, IQueryable<RssMessageModel>>
    {
        private readonly IRssMessagesRepository _rssMessagesRepository;
        
        public RssAllMessagesListAdapter(IQueryable<RssMessageModel> items, Activity activity, IRssMessagesRepository rssMessagesRepository) : base(items, activity)
        {
            _rssMessagesRepository = rssMessagesRepository;
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

                ImageService.Instance.LoadUrl(item.ImageUrl).Into(rssMessageViewHolder.ImageView);
                
                rssMessageViewHolder.ItemView.SetBackgroundColor(item.IsRead ? Color.Gray : Color.White);
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
            _rssMessagesRepository.ChangeIsFavorite(holderItem);
        }

        private void ReadItem(RssMessageModel holderItem)
        {
            _rssMessagesRepository.ChangeIsRead(holderItem);
        }
    }
}