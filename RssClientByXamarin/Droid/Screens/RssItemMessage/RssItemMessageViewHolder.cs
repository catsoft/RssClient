using System.ComponentModel;
using System.Globalization;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Autofac;
using FFImageLoading;
using FFImageLoading.Views;
using Shared;
using Shared.Database.Rss;
using Shared.Services.Locale;

namespace Droid.Screens.RssItemMessage
{
    public class RssItemMessageViewHolder : BaseRssMessageViewHolder
    {
        public TextView Title { get; }
        public TextView Text { get; }
        public TextView CreationDate { get; }
        public ImageViewAsync ImageView { get; }
        public LinearLayout ClickView { get; }
        public RssMessageModel Item { get; set; }
        public CardView CardView { get; }
        public LinearLayout Background { get; }
        
        public RssItemMessageViewHolder(View itemView) : base(itemView)
        {
            Title = itemView.FindViewById<TextView>(Resource.Id.textView_messagesItem_title);
            Text = itemView.FindViewById<TextView>(Resource.Id.textView_messagesItem_text);
            CreationDate = itemView.FindViewById<TextView>(Resource.Id.textView_messagesItem_date);
            ClickView = itemView.FindViewById<LinearLayout>(Resource.Id.linearLayout_messagesItem_content);
            ImageView = itemView.FindViewById<ImageViewAsync>(Resource.Id.imageView_messagesItem_image);
            CardView = itemView.FindViewById<CardView>(Resource.Id.cardView_messagesItem_card);
            Background = itemView.FindViewById<LinearLayout>(Resource.Id.linearLayout_messagesItem_background);
        }
        
        public override void BindData(RssMessageModel item)
        {
            if(Item != null)
                Item.PropertyChanged -= UpdateHimself;
            
            Item = item;
            
            var localeService = App.Container.Resolve<ILocale>();

            Title.Text = item.Title;
            Text.Text = item.Text;
            CreationDate.Text = item.CreationDate.ToString("d", new CultureInfo(localeService.GetCurrentLocaleId()));
            Background.SetBackgroundColor(item.IsRead ? BackgroundItemSelectColor : BackgroundItemColor);
            ImageView.Visibility = string.IsNullOrEmpty(item.Url) ? ViewStates.Gone : ViewStates.Visible;
            ImageService.Instance.LoadUrl(item.ImageUrl).Into(ImageView);

            item.PropertyChanged += UpdateHimself;
        }

        public void UpdateHimself(object sender, PropertyChangedEventArgs e)
        {
            BindData(Item);
        }
    }
}