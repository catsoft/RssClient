using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Droid.Screens.Base.Adapters;
using Shared.Services.Rss;

namespace Droid.Screens.RssEditList
{
    public class RssListEditViewHolder : RecyclerView.ViewHolder, IDataBind<RssServiceModel>
    {
        public RssServiceModel Item { get; set; }
        
        public TextView TitleTextView { get; }
        public TextView SubtitleTextView { get; }
        public ImageView DeleteImage { get; }
        public ImageView ReorderImage { get; }
        
        public RssListEditViewHolder(View itemView) : base(itemView)
        {
            TitleTextView = itemView.FindViewById<TextView>(Resource.Id.textView_listItemEditRss_title);
            SubtitleTextView = itemView.FindViewById<TextView>(Resource.Id.textView_listItemEditRss_subtitle);

            DeleteImage = itemView.FindViewById<ImageView>(Resource.Id.imageView_listItemEditRss_delete);
            ReorderImage = itemView.FindViewById<ImageView>(Resource.Id.imageView_listItemEditRss_reorder);
        }

        public void BindData(RssServiceModel item)
        {
            Item = item;
        }
    }
}