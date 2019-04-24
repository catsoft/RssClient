using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Core.Extensions;
using Core.Services.RssFeeds;
using Droid.NativeExtension;
using Droid.Screens.Base;
using Droid.Screens.Base.Adapters;
using FFImageLoading;
using FFImageLoading.Views;
using FFImageLoading.Work;
using JetBrains.Annotations;

namespace Droid.Screens.RssFeeds.List
{
    public class RssFeedListItemViewHolder : RecyclerView.ViewHolder, IDataBind<RssFeedServiceModel>, IShowAndLoadImage
    {
        public RssFeedListItemViewHolder([NotNull] View itemView, bool showImages) : base(itemView)
        {
            IsShowAndLoadImages = showImages;

            TitleTextView = itemView.FindViewById<TextView>(Resource.Id.textView_listItemRss_title).NotNull();
            SubtitleTextView = itemView.FindViewById<TextView>(Resource.Id.textView_listItemRss_subtitle).NotNull();
            ClickView = itemView.FindViewById<LinearLayout>(Resource.Id.linearLayout_listItemRss_content).NotNull();
            IconView = itemView.FindViewById<ImageViewAsync>(Resource.Id.imageView_listItemRss_rssIcon).NotNull();
            CountTextView = itemView.FindViewById<TextView>(Resource.Id.textView_listItemRss_rssCount).NotNull();

            IconView.Visibility = IsShowAndLoadImages.ToVisibility();
        }

        [NotNull] public TextView TitleTextView { get; }
        [NotNull] public TextView SubtitleTextView { get; }
        [NotNull] public ImageViewAsync IconView { get; }
        [NotNull] public TextView CountTextView { get; }
        [NotNull] public LinearLayout ClickView { get; }

        public RssFeedServiceModel Item { get; set; }

        public void BindData(RssFeedServiceModel item)
        {
            TitleTextView.Text = item.Name;
            Item = item;

            if (IsShowAndLoadImages)
                ImageService.Instance.NotNull()
                    .LoadUrl(item.UrlPreviewImage)
                    .NotNull()
                    .LoadingPlaceholder("no_image.png", ImageSource.CompiledResource)
                    .NotNull()
                    .ErrorPlaceholder("no_image.png", ImageSource.CompiledResource)
                    .Into(IconView);
        }

        public bool IsShowAndLoadImages { get; }
    }
}
