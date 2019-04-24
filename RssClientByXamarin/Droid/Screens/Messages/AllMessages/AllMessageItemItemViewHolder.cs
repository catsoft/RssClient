using Android.Views;
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

            Title = itemView.FindViewById<TextView>(Resource.Id.textView_allMessagesItem_title).NotNull();
            Text = itemView.FindViewById<TextView>(Resource.Id.textView_allMessagesItem_text).NotNull();
            CreationDate = itemView.FindViewById<TextView>(Resource.Id.textView_allMessagesItem_date).NotNull();
            Canal = itemView.FindViewById<TextView>(Resource.Id.textView_allMessagesItem_canal).NotNull();
            ClickView = itemView.FindViewById<LinearLayout>(Resource.Id.linearLayout_allMessagesItem_content).NotNull();
            ImageView = itemView.FindViewById<ImageViewAsync>(Resource.Id.imageView_allMessagesItem_image).NotNull();
            Background = itemView.FindViewById<LinearLayout>(Resource.Id.linearLayout_allMessagesItem_background).NotNull();
            RatingBar = itemView.FindViewById<RatingBar>(Resource.Id.ratingBar_allMessagesItem_favorite).NotNull();

            ImageView.Visibility = IsShowAndLoadImages.ToVisibility();
        }

        
        [NotNull] public TextView Title { get; }
        
        [NotNull] public TextView Text { get; }
        
        [NotNull] public TextView CreationDate { get; }
        
        [NotNull] public TextView Canal { get; }
        
        [NotNull] public ImageViewAsync ImageView { get; }
        
        [NotNull] public LinearLayout ClickView { get; }
        
        [NotNull] public LinearLayout Background { get; }
        
        [NotNull] public RatingBar RatingBar { get; }
        
        public bool IsShowAndLoadImages { get; }

        public override void BindData(RssMessageServiceModel item)
        {
            Item = item;

            Title.Text = item.Title;
            Text.SetTextAsHtml(item.Text);
            CreationDate.Text = item.CreationDate.ToShortDateLocaleString();
            // todo
//            Canal.Text = item.RssFeedParent?.Name;
            Background.SetBackgroundColor(item.IsRead ? BackgroundItemSelectColor : BackgroundItemColor);
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
