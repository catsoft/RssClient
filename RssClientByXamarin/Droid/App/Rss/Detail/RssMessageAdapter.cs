using Android.Support.V7.Widget;
using Android.Views;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Net;
using Database.Rss;

namespace RssClient.App.Rss.Detail
{
	public class RssMessageAdapter : RecyclerView.Adapter
    {
        private const string CreationDateFormat = "dd MMMM yyyy";

        private readonly Activity _activity;

        public RssMessageAdapter(IEnumerable<RssMessageModel> items, Activity activity)
        {
            _activity = activity;
            Items = items.OrderByDescending(w => w.CreationDate).ToList();
        }

        public List<RssMessageModel> Items { get; }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var item = Items[position];

            if (holder is RssMessageViewHolder rssMessageViewHolder)
            {
                rssMessageViewHolder.Title.Text = item.Title;
                rssMessageViewHolder.Text.Text = item.Text;
                rssMessageViewHolder.CreationDate.Text = item.CreationDate.ToString(CreationDateFormat, CultureInfo.CurrentCulture);
                rssMessageViewHolder.Item = item;
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.rss_message_item, parent, false);
            var holder = new RssMessageViewHolder(view);

            holder.ClickView.Click += (sender, args) => { OpenContentActivity(holder.Item); };

            return holder;
        }

        public override int ItemCount => Items.Count;


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