using Android.Views;
using Android.Widget;
using Droid.NativeExtension;
using Droid.Screens.Base;
using Droid.Screens.RssMessagesList;
using FFImageLoading;
using FFImageLoading.Views;
using JetBrains.Annotations;
using Shared.Extensions;
using Shared.Infrastructure.Locale;
using Shared.Repositories.RssMessage;

namespace Droid.Screens.RssAllMessages
{
    public class RssAllMessagesViewHolder : BaseRssMessagesViewHolder, IShowAndLoadImage
    {
        public RssAllMessagesViewHolder([NotNull] View itemView, bool isShowAndLoadImages) : base(itemView)
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

        public override void BindData([NotNull] RssMessageDomainModel item)
        {
            Item = item;

            Title.Text = item.Title;
            Text.SetTextAsHtml(item.Text);
            CreationDate.Text = item.CreationDate.ToShortDateLocaleString();
            Canal.Text = item.RssParent.Name;
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
