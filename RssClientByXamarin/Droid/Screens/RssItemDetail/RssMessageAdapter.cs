using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Autofac;
using Droid.Screens.RssMessage;
using FFImageLoading;
using Shared;
using Shared.Database.Rss;
using Shared.Repository;
using Shared.Services.Locale;

namespace Droid.Screens.RssItemDetail
{
    public class RssMessageAdapter : BaseRssMessageAdapter<List<RssMessageModel>>
    {
        public RssMessageAdapter(List<RssMessageModel> items, Activity activity, IRssMessagesRepository rssMessagesRepository) : base(items, activity, rssMessagesRepository)
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
                rssMessageViewHolder.Background.SetBackgroundColor(item.IsRead ? BackgroundItemSelectColor : BackgroundItemColor);
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
    }
}