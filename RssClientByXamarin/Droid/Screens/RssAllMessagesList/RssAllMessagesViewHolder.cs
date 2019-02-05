using System.ComponentModel;
using System.Globalization;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Autofac;
using Droid.Screens.RssItemMessage;
using FFImageLoading;
using FFImageLoading.Views;
using Shared;
using Shared.Database.Rss;
using Shared.Services.Locale;

namespace Droid.Screens.RssAllMessagesList
{
    public class RssAllMessagesViewHolder : BaseRssMessageViewHolder
    {
        public TextView Title { get; }
        public TextView Text { get; }
        public TextView CreationDate { get; }
        public TextView Canal { get; }
        public ImageViewAsync ImageView { get; }
        public LinearLayout ClickView { get; }
        public CardView CardView { get; }
        public LinearLayout Background { get; }

        public RssAllMessagesViewHolder(View itemView) : base(itemView)
        {
            Title = itemView.FindViewById<TextView>(Resource.Id.textView_allMessagesItem_title);
            Text = itemView.FindViewById<TextView>(Resource.Id.textView_allMessagesItem_text);
            CreationDate = itemView.FindViewById<TextView>(Resource.Id.textView_allMessagesItem_date);
            Canal = itemView.FindViewById<TextView>(Resource.Id.textView_allMessagesItem_canal);
            ClickView = itemView.FindViewById<LinearLayout>(Resource.Id.linearLayout_allMessagesItem_content);
            ImageView = itemView.FindViewById<ImageViewAsync>(Resource.Id.imageView_allMessagesItem_image);
            CardView = itemView.FindViewById<CardView>(Resource.Id.cardView_allMessagesItem_card);
            Background = itemView.FindViewById<LinearLayout>(Resource.Id.linearLayout_allMessagesItem_background);
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
                Text.Text = item.Text;
                CreationDate.Text = item.CreationDate.ToString("d", new CultureInfo(localeService.GetCurrentLocaleId()));
                Canal.Text = item.RssLink;
                ImageView.Visibility = string.IsNullOrEmpty(item.Url) ? ViewStates.Gone : ViewStates.Visible;

                Background.SetBackgroundColor(item.IsRead ? BackgroundItemSelectColor : BackgroundItemColor);

                ImageService.Instance.LoadUrl(item.ImageUrl).Into(ImageView);
                
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