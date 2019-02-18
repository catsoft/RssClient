using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Droid.Screens.Base.Adapters;
using Droid.Services.Helpers;
using Shared.Database.Rss;

namespace Droid.Screens.RecommendedCategoryList
{
    public class RssRecommendedCategoryViewHolder : RecyclerView.ViewHolder, IDataBind<Categories>
    {
        public Categories Item { get; set; }
        
        public TextView TitleView { get; }

        public RssRecommendedCategoryViewHolder(View itemView) : base(itemView)
        {
            TitleView = itemView.FindViewById<TextView>(Resource.Id.textView_headerRecommendedRssList_title);
        }

        public void BindData(Categories item)
        {
            Item = item;
            
            // TODO проверить каст
            TitleView.Text = item.ToLocaleString(this.ItemView.Context as Activity);
        }
    }
}