using Android.Support.V7.Widget;
using Android.Views;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Net;
using Com.Bumptech.Glide;
using Database.Rss;
using RssClient.Infrastructure.Locale;

namespace RssClient.App.Rss.Detail
{
	public class RssMessageAdapter : RecyclerView.Adapter
    {
        private readonly Activity _activity;
	    public List<RssMessageModel> Items { get; }

		public RssMessageAdapter(List<RssMessageModel> items, Activity activity)
        {
            _activity = activity;
	        Items = items;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var item = Items.ElementAt(position);

            if (holder is RssMessageViewHolder rssMessageViewHolder)
            {
                rssMessageViewHolder.Title.Text = item.Title;
                rssMessageViewHolder.Text.Text = item.Text;
                rssMessageViewHolder.CreationDate.Text = item.CreationDate.ToString("d", new CultureInfo(new Locale().GetCurrentLocaleId()));
                rssMessageViewHolder.Item = item;

                rssMessageViewHolder.ImageView.Visibility = string.IsNullOrEmpty(item.Url) ? ViewStates.Gone : ViewStates.Visible;
                Glide.With(_activity).Load(item.ImageUrl).Into(rssMessageViewHolder.ImageView);
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_item_rss_message, parent, false);
            var holder = new RssMessageViewHolder(view);

            holder.ClickView.Click += (sender, args) => { OpenContentActivity(holder.Item); };

            return holder;
        }

        public override int ItemCount => Items.Count();


        private void OpenContentActivity(RssMessageModel item)
        {
            if (!string.IsNullOrEmpty(item.Url))
            {
                var intent = new Intent(Intent.ActionView, Uri.Parse(item.Url));
                _activity.StartActivity(intent);
            }
        }
    }
}