using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using FFImageLoading.Views;
using Shared.Database.Rss;

namespace Droid.Screens.Rss.List.RssList
{
	public class RssListViewHolder : RecyclerView.ViewHolder
    {
        public RssListViewHolder(View itemView) : base(itemView)
        {
            TitleTextView = itemView.FindViewById<TextView>(Resource.Id.textView_listItemRss_title);
            SubtitleTextView = itemView.FindViewById<TextView>(Resource.Id.textView_listItemRss_subtitle);
            ClickView = itemView.FindViewById<LinearLayout>(Resource.Id.linearLayout_listItemRss_content);
            IconView = itemView.FindViewById<ImageViewAsync>(Resource.Id.imageView_listItemRss_rssIcon);
            CountTextView = itemView.FindViewById<TextView>(Resource.Id.textView_listItemRss_rssCount);
        }

        public TextView TitleTextView { get; }
        public TextView SubtitleTextView { get; }
        public RssModel Item { get; set; }
        public ImageViewAsync IconView { get; }
        public TextView CountTextView { get; }
        public LinearLayout ClickView { get; }
    }
}