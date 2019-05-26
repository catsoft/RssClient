using Android.Graphics;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Core.Extensions;
using Core.Infrastructure.Locale;
using Core.Services.RssMessages;
using Droid.NativeExtension;
using FFImageLoading;
using FFImageLoading.Cache;
using FFImageLoading.Views;
using JetBrains.Annotations;

namespace Droid.Screens.Messages.Book
{
    public class BookMessageViewHolder
    {
        private readonly Color _backgroundItemSelectColor = new Color(0, 0, 0, 95);
        private readonly Color _backgroundItemColor = new Color(0, 0, 0, 0);
        
        [NotNull] private readonly View _itemView;

        public BookMessageViewHolder([NotNull] View itemView)
        {
            _itemView = itemView;
            TextWebView = itemView.FindNotNull<WebView>(Resource.Id.webView_bookMessage_mainView);
            TitleTextView = itemView.FindNotNull<TextView>(Resource.Id.textView_bookMessage_title);
            CanalTextView = itemView.FindNotNull<TextView>(Resource.Id.textView_bookMessage_canal);
            MiniIconImageView = itemView.FindNotNull<ImageViewAsync>(Resource.Id.imageView_bookMessage_miniIcon);
            DateTextView = itemView.FindNotNull<TextView>(Resource.Id.textView_bookMessage_date);
            CountingTextView = itemView.FindNotNull<TextView>(Resource.Id.textView_bookMessage_counting);
            
            TextWebView.Init();
            TextWebView.DisableScroll();
            TextWebView.TurnLoadImages(true);
        }

        [NotNull] public WebView TextWebView { get; }
        
        [NotNull] public TextView TitleTextView { get; }
        
        [NotNull] public TextView CanalTextView { get; }
        
        [NotNull] public TextView DateTextView { get; }
        
        [NotNull] public ImageViewAsync MiniIconImageView { get; }
        
        [NotNull] public TextView CountingTextView { get; }
        
        public void Bind([NotNull] RssMessageServiceModel message)
        {
            TextWebView.Visibility = message.TextHtml.IsNotEmpty().ToVisibility();
            TextWebView.SetHtml(message.TextHtml);

            TitleTextView.Text = message.Title;
            CanalTextView.Text = message.RssTitle;
            DateTextView.Text = message.CreationDate.ToShortDateLocaleString();
            
            ImageService.Instance.NotNull()
                .LoadUrl(message.RssIcon)
                .NotNull()
                .WithCache(CacheType.All)
                .NotNull()
                .Into(MiniIconImageView);
            
            _itemView.SetBackgroundColor(message.IsRead ? _backgroundItemSelectColor : _backgroundItemColor);
        }

        public void SetCounting(int pagerCurrentItem, int adapterCount)
        {
            CountingTextView.Text = $"{pagerCurrentItem + 1}/{adapterCount}";
        }
    }
}