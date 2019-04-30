using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Core.Infrastructure.Locale;
using Core.Resources;
using Core.Services.RssFeeds;
using Droid.NativeExtension;
using Droid.Screens.Base.Adapters;
using JetBrains.Annotations;

namespace Droid.Screens.RssFeeds.EditableList
{
    public class RssFeedEditableListItemViewHolder : RecyclerView.ViewHolder, IDataBind<RssFeedServiceModel>
    {
        public RssFeedEditableListItemViewHolder([NotNull] View itemView) : base(itemView)
        {
            TitleTextView = itemView.FindNotNull<TextView>(Resource.Id.textView_listItemEditRss_title);
            SubtitleTextView = itemView.FindNotNull<TextView>(Resource.Id.textView_listItemEditRss_subtitle);
            DeleteImage = itemView.FindNotNull<ImageView>(Resource.Id.imageView_listItemEditRss_delete);
            ReorderImage = itemView.FindNotNull<ImageView>(Resource.Id.imageView_listItemEditRss_reorder);
        }

        [NotNull] public TextView TitleTextView { get; }

        [NotNull] public TextView SubtitleTextView { get; }

        [NotNull] public ImageView DeleteImage { get; }

        [NotNull] public ImageView ReorderImage { get; }

        public RssFeedServiceModel Item { get; private set; }

        public void BindData(RssFeedServiceModel item)
        {
            Item = item;
            
            SubtitleTextView.Text = item.UpdateTime == null
                ? Strings.RssFeedItemNotUpdated
                : $"{Strings.RssFeedItemUpdated} {item.UpdateTime.Value.ToShortGeneralLocaleString()}";
            TitleTextView.Text = item.Name;
        }
    }
}
