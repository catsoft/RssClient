using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using FFImageLoading.Views;
using Shared.Database.Rss;

namespace Droid.Screens.RssEditList
{
    public class RssListEditViewHolder : RecyclerView.ViewHolder
    {
        public RssListEditViewHolder(View itemView) : base(itemView)
        {
            TitleTextView = itemView.FindViewById<TextView>(Resource.Id.textView_listItemEditRss_title);
            SubtitleTextView = itemView.FindViewById<TextView>(Resource.Id.textView_listItemEditRss_subtitle);
            IconView = itemView.FindViewById<ImageViewAsync>(Resource.Id.imageView_listItemEditRss_rssIcon);
        }

        public TextView TitleTextView { get; }
        public TextView SubtitleTextView { get; }
        public RssModel Item { get; set; }
        public ImageViewAsync IconView { get; }
    }
}