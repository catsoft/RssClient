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

            DeleteImage = itemView.FindViewById<ImageView>(Resource.Id.imageView_listItemEditRss_delete);
            ReorderImage = itemView.FindViewById<ImageView>(Resource.Id.imageView_listItemEditRss_reorder);
        }

        public TextView TitleTextView { get; }
        public TextView SubtitleTextView { get; }
        public RssModel Item { get; set; }
        public ImageView DeleteImage { get; set; }
        public ImageView ReorderImage { get; set; }
    }
}