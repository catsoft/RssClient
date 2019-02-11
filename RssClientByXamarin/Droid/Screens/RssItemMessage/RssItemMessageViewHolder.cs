using System.ComponentModel;
using System.Globalization;
using Android.Media;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Autofac;
using Droid.NativeExtension;
using Droid.Screens.Base;
using FFImageLoading;
using FFImageLoading.Views;
using Shared;
using Shared.Database.Rss;
using Shared.Services.Locale;

namespace Droid.Screens.RssItemMessage
{
    public class RssItemMessageViewHolder : BaseRssMessageViewHolder, IShowAndLoadImage
    {
        public bool IsShowAndLoadImages { get; }
        
        public TextView Title { get; }
        public TextView Text { get; }
        public TextView CreationDate { get; }
        public ImageViewAsync ImageView { get; }
        public LinearLayout ClickView { get; }
        public RssMessageModel Item { get; set; }
        public CardView CardView { get; }
        public LinearLayout Background { get; }
        
        public RssItemMessageViewHolder(View itemView, bool isShowAndLoadImages) : base(itemView)
        {
            IsShowAndLoadImages = isShowAndLoadImages;
            Title = itemView.FindViewById<TextView>(Resource.Id.textView_messagesItem_title);
            Text = itemView.FindViewById<TextView>(Resource.Id.textView_messagesItem_text);
            CreationDate = itemView.FindViewById<TextView>(Resource.Id.textView_messagesItem_date);
            ClickView = itemView.FindViewById<LinearLayout>(Resource.Id.linearLayout_messagesItem_content);
            ImageView = itemView.FindViewById<ImageViewAsync>(Resource.Id.imageView_messagesItem_image);
            CardView = itemView.FindViewById<CardView>(Resource.Id.cardView_messagesItem_card);
            Background = itemView.FindViewById<LinearLayout>(Resource.Id.linearLayout_messagesItem_background);

            ImageView.Visibility = isShowAndLoadImages.ToVisibility();
        }
        
        public override void BindData(RssMessageModel item)
        {
            if(Item != null)
                Item.PropertyChanged -= UpdateHimself;
            
            Item = item;

            // TODO бывает что диспознутое. Когда тыкаешь и тыкаешь.
            if (Item.IsValid)
            {
                var localeService = App.Container.Resolve<ILocale>();

                Title.Text = item.Title;
                Text.Text = item.Text;
                CreationDate.Text = item.CreationDate.ToString("d", new CultureInfo(localeService.GetCurrentLocaleId()));
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