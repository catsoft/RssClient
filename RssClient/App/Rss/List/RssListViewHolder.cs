using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Shared.App.Rss;

namespace RssClient.App.Rss.List
{
    public class RssListViewHolder : RecyclerView.ViewHolder
    {
        public TextView TitleTextView { get; }
        public TextView SubtitleTextView { get; }
        public RssModel Item { get; set; }

        public RssListViewHolder(View itemView) : base(itemView)
        {
            TitleTextView = itemView.FindViewById<TextView>(Resource.Id.rss_item_title);
            SubtitleTextView = itemView.FindViewById<TextView>(Resource.Id.rss_item_subtitle);
        }
    }
}