using System.ComponentModel;
using System.Globalization;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Autofac;
using Droid.NativeExtension;
using Droid.Screens.Base;
using Droid.Screens.RssItemMessage;
using FFImageLoading;
using FFImageLoading.Views;
using Shared;
using Shared.Database.Rss;
using Shared.Services.Locale;

namespace Droid.Screens.RssAllMessagesList
{
    public class RssAllMessagesViewHolder : BaseRssMessageViewHolder, IShowAndLoadImage
    {
        public bool IsShowAndLoadImages { get; }
        
        public TextView Title { get; }
        public TextView Text { get; }
        public TextView CreationDate { get; }
        public TextView Canal { get; }
        public ImageViewAsync ImageView { get; }
        public LinearLayout ClickView { get; }
        public CardView CardView { get; }
        public LinearLayout Background { get; }

        public RssAllMessagesViewHolder(View itemView, bool isShowAndLoadImages) : base(itemView)
        {
            IsShowAndLoadImages = isShowAndLoadImages;
            
            Title = itemView.FindViewById<TextView>(Resource.Id.textView_allMessagesItem_title);
            Text = itemView.FindViewById<TextView>(Resource.Id.textView_allMessagesItem_text);
            CreationDate = itemView.FindViewById<TextView>(Resource.Id.textView_allMessagesItem_date);
            Canal = itemView.FindViewById<TextView>(Resource.Id.textView_allMessagesItem_canal);
            ClickView = itemView.FindViewById<LinearLayout>(Resource.Id.linearLayout_allMessagesItem_content);
            ImageView = itemView.FindViewById<ImageViewAsync>(Resource.Id.imageView_allMessagesItem_image);
            CardView = itemView.FindViewById<CardView>(Resource.Id.cardView_allMessagesItem_card);
            Background = itemView.FindViewById<LinearLayout>(Resource.Id.linearLayout_allMessagesItem_background);

            ImageView.Visibility = IsShowAndLoadImages.ToVisibility();
        }

        public override void BindData(RssMessageModel item)
        {
            if (Item != null)
                Item.PropertyChanged -= UpdateHimself;
            
            Item = item;

            if (Item.IsValid)
            {
                var localeService = App.Container.Resolve<ILocale>();

                Title.Text = item.Title;
                Text.SetTextAsHtml(item.Text);
                CreationDate.Text = item.CreationDate.ToString("d", new CultureInfo(localeService.GetCurrentLocaleId()));
                Canal.Text = item.RssLink;
                
                Background.SetBackgroundColor(item.IsRead ? BackgroundItemSelectColor : BackgroundItemColor);

                if (IsShowAndLoadImages)
                {
                    ImageView.Visibility = string.IsNullOrEmpty(item.Url).ToVisibility();
                    ImageService.Instance.LoadUrl(item.ImageUrl).Into(ImageView);
                }
                
                item.PropertyChanged += UpdateHimself;
            }
            else
            {
                item.PropertyChanged -= UpdateHimself;
            }
        }
        
        private void UpdateHimself(object sender, PropertyChangedEventArgs e)
        {
            BindData(Item);
        }
    }
}