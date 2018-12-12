using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Database.Rss;

namespace RssClient.App.Rss.Detail
{
	public class RssMessageViewHolder : RecyclerView.ViewHolder
    {
        public RssMessageViewHolder(View itemView) : base(itemView)
        {
            Title = itemView.FindViewById<TextView>(Resource.Id.rss_message_title);
            Text = itemView.FindViewById<TextView>(Resource.Id.rss_message_text);
            CreationDate = itemView.FindViewById<TextView>(Resource.Id.rss_message_date);
            ClickView = itemView.FindViewById<LinearLayout>(Resource.Id.rss_message_click_view);
        }

        public TextView Title { get; }
        public TextView Text { get; }
        public TextView CreationDate { get; }
        public RssMessageModel Item { get; set; }
        public LinearLayout ClickView { get; set; }
    }
}