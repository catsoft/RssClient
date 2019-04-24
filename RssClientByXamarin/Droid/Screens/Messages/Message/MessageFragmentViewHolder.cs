using Android.Views;
using Android.Webkit;
using Core.Extensions;
using JetBrains.Annotations;

namespace Droid.Screens.Messages.Message
{
    public class MessageFragmentViewHolder
    {
        public MessageFragmentViewHolder([NotNull] View view)
        {
            WebView = view.FindViewById<WebView>(Resource.Id.webView_rssMessage_mainView).NotNull();

            WebView.ScrollBarStyle = ScrollbarStyles.OutsideOverlay;
            WebView.ScrollbarFadingEnabled = false;

            var settings = WebView.Settings;
            settings.JavaScriptEnabled = true;
            settings.BuiltInZoomControls = true;
            settings.SetSupportZoom(true);

            var client = new WebViewClient();
            WebView.SetWebViewClient(client);
        }
        
        [NotNull] public WebView WebView { get; }
    }
}