using Android.Views;
using Android.Webkit;
using Core.Extensions;
using Core.Services.RssMessages;
using Droid.NativeExtension;

namespace Droid.Screens.Messages.Book
{
    public class BookMessageViewHolder
    {
        public BookMessageViewHolder(View itemView)
        {
            TextViewView = itemView.FindNotNull<WebView>(Resource.Id.webView_bookMessage_mainView);
        }

        public WebView TextViewView { get; }
        
        public void Bind(RssMessageServiceModel message)
        {
            TextViewView.Visibility = message.TextHtml.IsNotEmpty().ToVisibility();
            TextViewView.SetHtml(message.TextHtml);
        }
    }
}