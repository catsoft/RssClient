using Android.Views;
using Android.Webkit;
using Android.Widget;
using Core.Extensions;
using Core.Infrastructure.Locale;
using Core.Services.RssMessages;
using Droid.NativeExtension;
using Droid.Screens.Base;
using FFImageLoading;
using FFImageLoading.Cache;
using FFImageLoading.Views;
using JetBrains.Annotations;

namespace Droid.Screens.Messages.AllMessages
{
    public class AllMessageListItemViewHolder : BaseMessageItemViewHolder, IShowAndLoadImage
    {
        private bool _isShowContent;

        public AllMessageListItemViewHolder([NotNull] View itemView, bool isShowAndLoadImages) : base(itemView)
        {
            IsShowAndLoadImages = isShowAndLoadImages;

            TitleTextView = itemView.FindNotNull<TextView>(Resource.Id.textView_allMessagesItem_title);
            TextWebView = itemView.FindNotNull<WebView>(Resource.Id.webView_allMessagesItem_text);
            CreationDateTextView = itemView.FindNotNull<TextView>(Resource.Id.textView_allMessagesItem_date);
            CanalTextView = itemView.FindNotNull<TextView>(Resource.Id.textView_allMessagesItem_canal);
            MiniIconImageView = itemView.FindNotNull<ImageViewAsync>(Resource.Id.imageView_allMessagesIcon_miniIcon);
            RootRelativeLayout = itemView.FindNotNull<RelativeLayout>(Resource.Id.relativeLayout_allMessagesItem_root);
            ImageView = itemView.FindNotNull<ImageViewAsync>(Resource.Id.imageView_allMessagesItem_image);
            RatingBar = itemView.FindNotNull<RatingBar>(Resource.Id.ratingBar_allMessagesItem_favorite);

            ImageView.Visibility = IsShowAndLoadImages.ToVisibility();
            TextWebView.Init();
            TextWebView.DisableScroll();
            TextWebView.TurnLoadImages(isShowAndLoadImages);

            IsShowContent = true;
        }

        [NotNull] public TextView TitleTextView { get; }

        [NotNull] public WebView TextWebView { get; }

        [NotNull] public TextView CreationDateTextView { get; }

        [NotNull] public TextView CanalTextView { get; }

        [NotNull] public ImageViewAsync MiniIconImageView { get; }

        [NotNull] public ImageViewAsync ImageView { get; }

        [NotNull] public RelativeLayout RootRelativeLayout { get; }

        [NotNull] public RatingBar RatingBar { get; }

        public bool IsShowAndLoadImages { get; }

        public bool IsShowContent
        {
            get => _isShowContent;
            set
            {
                _isShowContent = value;
                var visibility = value.ToVisibility();
                TextWebView.Visibility = visibility;
                ImageView.Visibility = visibility;
            }
        }

        public override void BindData(RssMessageServiceModel item)
        {
            Item = item;

            TitleTextView.Text = item.Title;
            TitleTextView.Visibility = item.Title.IsNotEmpty().ToVisibility();
            CreationDateTextView.Text = item.CreationDate.ToShortDateLocaleString();
            CanalTextView.Text = item.RssTitle;
            RootRelativeLayout.SetBackgroundColor(item.IsRead ? BackgroundItemSelectColor : BackgroundItemColor);
            RatingBar.Rating = item.IsFavorite ? 1 : 0;
            RatingBar.Visibility = item.IsFavorite.ToVisibility();
            MiniIconImageView.Visibility = IsShowAndLoadImages.ToVisibility();

            if (IsShowAndLoadImages)
            {
                ImageView.Visibility = (!string.IsNullOrEmpty(item.Url)).ToVisibility();

                if (IsShowContent)
                    ImageService.Instance
                        .NotNull()
                        .LoadUrl(item.ImageUrl)
                        .Into(ImageView);

                ImageService.Instance.NotNull()
                    .LoadUrl(item.RssIcon)
                    .NotNull()
                    .WithCache(CacheType.All)
                    .NotNull()
                    .Into(MiniIconImageView);
            }

            if (IsShowContent)
            {
                TextWebView.SetHtml(item.TextHtml);
                TextWebView.Visibility = item.TextHtml.IsNotEmpty().ToVisibility();
            }
        }
    }
}
