using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Core.Extensions;
using Core.Services.RssFeeds;
using Droid.Resources;
using Droid.Screens.Base.Adapters;
using JetBrains.Annotations;

namespace Droid.Screens.RssFeeds.EditableList
{
    public class RssFeedEditableListItemViewHolder : RecyclerView.ViewHolder, IDataBind<RssFeedServiceModel>
    {
        public RssFeedEditableListItemViewHolder([NotNull] View itemView) : base(itemView)
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

        public RssFeedServiceModel Item { get; private set; }

        public void BindData(RssFeedServiceModel item) { Item = item; }
    }
}
