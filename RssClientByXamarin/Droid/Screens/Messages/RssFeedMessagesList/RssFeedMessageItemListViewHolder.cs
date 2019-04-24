using Android.Views;
using Android.Widget;
using Core.Infrastructure.Locale;
using Core.Services.RssMessages;
using Droid.NativeExtension;
using Droid.Resources;
using Droid.Screens.Base;
using FFImageLoading;
using FFImageLoading.Views;

namespace Droid.Screens.Messages.RssFeedMessagesList
{
    public class RssFeedMessageItemListViewHolder : BaseMessageItemViewHolder, IShowAndLoadImage
    {
        public RssFeedMessageItemListViewHolder(View itemView, bool isShowAndLoadImages) : base(itemView)
        {
            IsShowAndLoadImages = isShowAndLoadImages;
            Title = itemView.FindViewById<TextView>(Resource.Id.textView_messagesItem_title);
            Text = itemView.FindViewById<TextView>(Resource.Id.textView_messagesItem_text);
            CreationDate = itemView.FindViewById<TextView>(Resource.Id.textView_messagesItem_date);
            ClickView = itemView.FindViewById<LinearLayout>(Resource.Id.linearLayout_messagesItem_content);
            ImageView = itemView.FindViewById<ImageViewAsync>(Resource.Id.imageView_messagesItem_image);
            Background = itemView.FindViewById<LinearLayout>(Resource.Id.linearLayout_messagesItem_background);
            RatingBar = itemView.FindViewById<RatingBar>(Resource.Id.ratingBar_messagesItem_favorite);

            ImageView.Visibility = isShowAndLoadImages.ToVisibility();
        }

        public TextView Title { get; }
        public TextView Text { get; }
        public TextView CreationDate { get; }
        public ImageViewAsync ImageView { get; }
        public LinearLayout ClickView { get; }
        public LinearLayout Background { get; }
        public RatingBar RatingBar { get; }
        public bool IsShowAndLoadImages { get; }

        public override void BindData(RssMessageServiceModel item)
        {
            Item = item;

            Title.Text = item.Title;
            Text.SetTextAsHtml(item.Text);
            CreationDate.Text = item.CreationDate.ToShortDateLocaleString();
            Background.SetBackgroundColor(item.IsRead ? BackgroundItemSelectColor : BackgroundItemColor);
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
