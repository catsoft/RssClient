using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Core.Extensions;
using Core.Infrastructure.Locale;
using Core.Resources;
using Core.Services.RssFeeds;
using Droid.NativeExtension;
using Droid.Screens.Base;
using Droid.Screens.Base.Adapters;
using FFImageLoading;
using FFImageLoading.Cache;
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

            TitleTextView = itemView.FindNotNull<TextView>(Resource.Id.textView_listItemRss_title);
            SubtitleTextView = itemView.FindNotNull<TextView>(Resource.Id.textView_listItemRss_subtitle);
            ClickView = itemView.FindNotNull<CardView>(Resource.Id.cardView_listItemRss_content);
            IconView = itemView.FindNotNull<ImageViewAsync>(Resource.Id.imageView_listItemRss_rssIcon);
            CountTextView = itemView.FindNotNull<TextView>(Resource.Id.textView_listItemRss_rssCount);

            IconView.Visibility = IsShowAndLoadImages.ToVisibility();
        }

        [NotNull] public TextView TitleTextView { get; }
        
        [NotNull] public TextView SubtitleTextView { get; }
        
        [NotNull] public ImageViewAsync IconView { get; }
        
        [NotNull] public TextView CountTextView { get; }
        
        [NotNull] public CardView ClickView { get; }

        public RssFeedServiceModel Item { get; set; }

        public void BindData(RssFeedServiceModel item)
        {
            TitleTextView.Text = item.Name;
            Item = item;

            SubtitleTextView.Text = item.UpdateTime == null
                ? Strings.RssFeedItemNotUpdated
                : $"{Strings.RssFeedItemUpdated} {item.UpdateTime.Value.ToShortGeneralLocaleString()}";
            CountTextView.Text = item.CountNewMessages.ToString();
            
            if (IsShowAndLoadImages)
                ImageService.Instance.NotNull()
                    .LoadUrl(item.UrlPreviewImage)
                    .WithCache(CacheType.All)
                    .NotNull()
                    .LoadingPlaceholder("no_image.png", ImageSource.CompiledResource)
                    .NotNull()
                    .ErrorPlaceholder("no_image.png", ImageSource.CompiledResource)
                    .Into(IconView);
        }

        public bool IsShowAndLoadImages { get; }
    }
}
