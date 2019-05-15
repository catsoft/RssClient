using Android.Views;
using Android.Webkit;
using Android.Widget;
using Core.Extensions;
using Core.Infrastructure.Locale;
using Core.Services.RssMessages;
using Droid.NativeExtension;
using Droid.Screens.Base;
using FFImageLoading;
using FFImageLoading.Views;
using JetBrains.Annotations;

namespace Droid.Screens.Messages.RssFeedMessagesList
{
    public class RssFeedMessageItemListViewHolder : BaseMessageItemViewHolder, IShowAndLoadImage
    {
        private bool _isShowContent;

        public RssFeedMessageItemListViewHolder([NotNull] View itemView, bool isShowAndLoadImages) : base(itemView)
        {
            IsShowAndLoadImages = isShowAndLoadImages;
            TitleTextView = itemView.FindNotNull<TextView>(Resource.Id.textView_messagesItem_title);
            TextWebView = itemView.FindNotNull<WebView>(Resource.Id.webView_messagesItem_text);
            CreationDateTextView = itemView.FindNotNull<TextView>(Resource.Id.textView_messagesItem_date);
            RootRelativeLayout = itemView.FindNotNull<RelativeLayout>(Resource.Id.relativeLayout_messagesItem_root);
            ImageView = itemView.FindNotNull<ImageViewAsync>(Resource.Id.imageView_messagesItem_image);
            RatingBar = itemView.FindNotNull<RatingBar>(Resource.Id.ratingBar_messagesItem_favorite);

            ImageView.Visibility = isShowAndLoadImages.ToVisibility();
            
            TextWebView.Init();
            TextWebView.DisableScroll();
            TextWebView.TurnLoadImages(isShowAndLoadImages);

            IsShowContent = true;
        }

        [NotNull] public TextView TitleTextView { get; }
        
        [NotNull] public WebView TextWebView { get; }
        
        [NotNull] public TextView CreationDateTextView { get; }
        
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
            RootRelativeLayout.SetBackgroundColor(item.IsRead ? BackgroundItemSelectColor : BackgroundItemColor);
            RatingBar.Rating = item.IsFavorite ? 1 : 0;
            RatingBar.Visibility = item.IsFavorite.ToVisibility();

            if (IsShowAndLoadImages && IsShowContent)
            {
                ImageView.Visibility = (!string.IsNullOrEmpty(item.Url)).ToVisibility();
                ImageService.Instance.LoadUrl(item.ImageUrl).Into(ImageView);
            }

            if (IsShowContent)
            {
                TextWebView.SetHtml(item.TextHtml);
                TextWebView.Visibility = item.TextHtml.IsNotEmpty().ToVisibility();
            }
        }
    }
}
