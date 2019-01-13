using System.Globalization;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Autofac;
using Droid.Screens.Base.Adapters;
using FFImageLoading;
using Shared;
using Shared.Database.Rss;
using Shared.Services.Locale;
using Uri = Android.Net.Uri;

namespace Droid.Screens.RssAllMessagesList
{
	public class RssAllMessagesListAdapter : WithItemsAdapter<RssMessageModel, IQueryable<RssMessageModel>>
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

            holder.ClickView.Click += (sender, args) => { OpenContentActivity(holder.Item); };

            return holder;
        }

        private void OpenContentActivity(RssMessageModel item)
        {
            if (!string.IsNullOrEmpty(item.Url))
            {
                var intent = new Intent(Intent.ActionView, Uri.Parse(item.Url));
                Activity.StartActivity(intent);
            }
        }
    }
}