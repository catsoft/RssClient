using Android.Views;
using Android.Webkit;
using Droid.NativeExtension;
using JetBrains.Annotations;

namespace Droid.Screens.Messages.Message
{
    public class MessageFragmentViewHolder
    {
        public MessageFragmentViewHolder([NotNull] View view)
        {
            WebView = view.FindNotNull<WebView>(Resource.Id.webView_rssMessage_mainView);

            WebView.ScrollBarStyle = ScrollbarStyles.OutsideOverlay;
            WebView.ScrollbarFadingEnabled = false;

            var settings = WebView.Settings;
            settings.JavaScriptEnabled = true;
            settings.BuiltInZoomControls = true;
            settings.SetSupportZoom(true);
            //todo включать выключать по настройке изображений
            settings.DomStorageEnabled = true;
            settings.SetAppCacheEnabled(true);
            settings.LoadsImagesAutomatically = true;
            settings.MixedContentMode = MixedContentHandling.AlwaysAllow;
            
            var client = new WebViewClient();
            WebView.SetWebViewClient(client);
        }
        
        [NotNull] public WebView WebView { get; }
    }
}