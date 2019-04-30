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

namespace Droid.Screens.Messages.AllMessages
{
    public class AllMessageItemItemViewHolder : BaseMessageItemViewHolder, IShowAndLoadImage
    {
        public AllMessageItemItemViewHolder([NotNull] View itemView, bool isShowAndLoadImages) : base(itemView)
        {
            IsShowAndLoadImages = isShowAndLoadImages;

            TitleTextView = itemView.FindNotNull<TextView>(Resource.Id.textView_allMessagesItem_title);
            TextWebView = itemView.FindNotNull<WebView>(Resource.Id.webView_allMessagesItem_text);
            CreationDateTextView = itemView.FindNotNull<TextView>(Resource.Id.textView_allMessagesItem_date);
            CanalTextView = itemView.FindNotNull<TextView>(Resource.Id.textView_allMessagesItem_canal);
            ClickViewLinearLayout = itemView.FindNotNull<LinearLayout>(Resource.Id.linearLayout_allMessagesItem_content);
            ImageView = itemView.FindNotNull<ImageViewAsync>(Resource.Id.imageView_allMessagesItem_image);
            BackgroundLinearLayout = itemView.FindNotNull<LinearLayout>(Resource.Id.linearLayout_allMessagesItem_background);
            RatingBar = itemView.FindNotNull<RatingBar>(Resource.Id.ratingBar_allMessagesItem_favorite);

            ImageView.Visibility = IsShowAndLoadImages.ToVisibility();
            TextWebView.Init();
            TextWebView.DisableScroll();
        }

        [NotNull] public TextView TitleTextView { get; }
        
        [NotNull] public WebView TextWebView { get; }
        
        [NotNull] public TextView CreationDateTextView { get; }
        
        [NotNull] public TextView CanalTextView { get; }
        
        [NotNull] public ImageViewAsync ImageView { get; }
        
        [NotNull] public LinearLayout ClickViewLinearLayout { get; }
        
        [NotNull] public LinearLayout BackgroundLinearLayout { get; }
        
        [NotNull] public RatingBar RatingBar { get; }
        
        public bool IsShowAndLoadImages { get; }

        public override void BindData(RssMessageServiceModel item)
        {
            Item = item;

            TitleTextView.Text = item.Title;
            TextWebView.SetHtml(item.TextHtml);
            CreationDateTextView.Text = item.CreationDate.ToShortDateLocaleString();
            CanalTextView.Text = item.RssTitle;
            BackgroundLinearLayout.SetBackgroundColor(item.IsRead ? BackgroundItemSelectColor : BackgroundItemColor);
            RatingBar.Rating = item.IsFavorite ? 1 : 0;
            RatingBar.Visibility = item.IsFavorite.ToVisibility();

            if (IsShowAndLoadImages)
            {
                ImageView.Visibility = (!string.IsNullOrEmpty(item.Url)).ToVisibility();
                ImageService.Instance.NotNull().LoadUrl(item.ImageUrl).Into(ImageView);
            }
        }
    }
}
