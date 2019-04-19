#region

using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Droid.Screens.Base.Adapters;
using JetBrains.Annotations;
using Shared.Extensions;
using Shared.Services.Rss;

#endregion

namespace Droid.Screens.RssEditList
{
    public class RssListEditViewHolder : RecyclerView.ViewHolder, IDataBind<RssServiceModel>
    {
        public RssListEditViewHolder([NotNull] View itemView) : base(itemView)
        {
            TitleTextView = itemView.FindViewById<TextView>(Resource.Id.textView_listItemEditRss_title).NotNull();
            SubtitleTextView = itemView.FindViewById<TextView>(Resource.Id.textView_listItemEditRss_subtitle).NotNull();
            DeleteImage = itemView.FindViewById<ImageView>(Resource.Id.imageView_listItemEditRss_delete).NotNull();
            ReorderImage = itemView.FindViewById<ImageView>(Resource.Id.imageView_listItemEditRss_reorder).NotNull();
        }

        [NotNull] public TextView TitleTextView { get; }

        [NotNull] public TextView SubtitleTextView { get; }

        [NotNull] public ImageView DeleteImage { get; }

        [NotNull] public ImageView ReorderImage { get; }

        public RssServiceModel Item { get; private set; }

        public void BindData(RssServiceModel item) { Item = item; }
    }
}
