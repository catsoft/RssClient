using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using FFImageLoading.Views;

namespace Droid.Screens.RecommendedRssList
{
    public class RssRecommendedViewHolder : RecyclerView.ViewHolder
    {
        public ImageViewAsync RssIcon { get; }
        public TextView TitleView { get; }
        public ImageButton AddImageView { get; }
        public string RssUrl { get; set; }

        public RssRecommendedViewHolder(View itemView) : base(itemView)
        {
            RssIcon = itemView.FindViewById<ImageViewAsync>(Resource.Id.imageView_rssRecommendedItem_rssIcon);
            TitleView = itemView.FindViewById<TextView>(Resource.Id.textView_rssRecommendedItem_title);
            AddImageView = itemView.FindViewById<ImageButton>(Resource.Id.imageButton_rssRecommendedItem_add);
        }
    }
}