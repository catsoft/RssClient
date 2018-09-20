using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace RssClient.App.Rss.Detail
{
    public class RssMessageViewHolder : RecyclerView.ViewHolder
    {
        public TextView Title { get; }
        public TextView Text { get; }
        public TextView CreationDate { get; }
        public RssMessageModel Item { get; set; }

        public RssMessageViewHolder(View itemView) : base(itemView)
        {
            Title = itemView.FindViewById<TextView>(Resource.Id.rss_message_title);
            Text = itemView.FindViewById<TextView>(Resource.Id.rss_message_text);
            CreationDate = itemView.FindViewById<TextView>(Resource.Id.rss_message_date);
        }
    }
}