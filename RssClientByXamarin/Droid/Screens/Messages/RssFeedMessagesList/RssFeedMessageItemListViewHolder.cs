using Android.Views;
using Android.Webkit;
using Android.Widget;
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
        public RssFeedMessageItemListViewHolder([NotNull] View itemView, bool isShowAndLoadImages) : base(itemView)
        {
            IsShowAndLoadImages = isShowAndLoadImages;
            TitleTextView = itemView.FindNotNull<TextView>(Resource.Id.textView_messagesItem_title);
            TextWebView = itemView.FindNotNull<WebView>(Resource.Id.webView_messagesItem_text);
            CreationDateTextView = itemView.FindNotNull<TextView>(Resource.Id.textView_messagesItem_date);
            RootRelativeLayout = itemView.FindNotNull<LinearLayout>(Resource.Id.relativeLayout_messagesItem_root);
            ImageView = itemView.FindNotNull<ImageViewAsync>(Resource.Id.imageView_messagesItem_image);
            RatingBar = itemView.FindNotNull<RatingBar>(Resource.Id.ratingBar_messagesItem_favorite);

            ImageView.Visibility = isShowAndLoadImages.ToVisibility();
            
            TextWebView.Init();
            TextWebView.DisableScroll();
            TextWebView.TurnLoadImages(isShowAndLoadImages);
        }

        [NotNull] public TextView TitleTextView { get; }
        
        [NotNull] public WebView TextWebView { get; }
        
        [NotNull] public TextView CreationDateTextView { get; }
        
        [NotNull] public ImageViewAsync ImageView { get; }
        
        [NotNull] public LinearLayout RootRelativeLayout { get; }
        
        [NotNull] public RatingBar RatingBar { get; }
        
        public bool IsShowAndLoadImages { get; }

        public override void BindData(RssMessageServiceModel item)
        {
            Item = item;

            TitleTextView.Text = item.Title;
            TextWebView.SetHtml(item.TextHtml);
            CreationDateTextView.Text = item.CreationDate.ToShortDateLocaleString();
            RootRelativeLayout.SetBackgroundColor(item.IsRead ? BackgroundItemSelectColor : BackgroundItemColor);
            RatingBar.Rating = item.IsFavorite ? 1 : 0;
            RatingBar.Visibility = item.IsFavorite.ToVisibility();

            if (IsShowAndLoadImages)
            {
                ImageView.Visibility = (!string.IsNullOrEmpty(item.Url)).ToVisibility();
                ImageService.Instance.LoadUrl(item.ImageUrl).Into(ImageView);
            }
        }
    }
}
