using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Shared.Database.Rss;

namespace Droid.Screens.RecommendedCategoryList
{
    public class RssRecommendedCategoryViewHolder : RecyclerView.ViewHolder
    {
        public TextView TitleView { get; }
        public Categories Categories { get; set; }

        public RssRecommendedCategoryViewHolder(View itemView) : base(itemView)
        {
            TitleView = itemView.FindViewById<TextView>(Resource.Id.textView_headerRecommendedRssList_title);
        }
    }
}