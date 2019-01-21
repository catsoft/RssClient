using System.Net.Mime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Droid.Screens.RecommendedRssList
{
    public class RssRecommendedHeaderViewHolder : RecyclerView.ViewHolder
    {
        public TextView TitleView { get; }
        
        public RssRecommendedHeaderViewHolder(View itemView) : base(itemView)
        {
            TitleView = itemView.FindViewById<TextView>(Resource.Id.textView_headerRecommendedRssList_title);
        }
    }
}