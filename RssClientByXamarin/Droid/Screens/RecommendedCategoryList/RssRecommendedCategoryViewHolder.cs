using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Droid.Screens.RecommendedCategoryList
{
    public class RssRecommendedCategoryViewHolder : RecyclerView.ViewHolder
    {
        public TextView TitleView { get; }
        
        public RssRecommendedCategoryViewHolder(View itemView) : base(itemView)
        {
            TitleView = itemView.FindViewById<TextView>(Resource.Id.recyclerView_rssRecommendedCategoryList_list);
        }
    }
}