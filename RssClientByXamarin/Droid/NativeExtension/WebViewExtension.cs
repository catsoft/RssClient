using Android.Views;
using Android.Webkit;
using JetBrains.Annotations;

namespace Droid.NativeExtension
{
    public static class WebViewExtension
    {
        public static void Init([NotNull] this WebView webView)
        {
            webView.ScrollBarStyle = ScrollbarStyles.OutsideOverlay;
            webView.ScrollbarFadingEnabled = false;
            
            var settings = webView.Settings;
            settings.JavaScriptEnabled = true;
            settings.BuiltInZoomControls = true;
            settings.SetSupportZoom(true);
            settings.DomStorageEnabled = true;
            settings.SetAppCacheEnabled(true);
            settings.LoadsImagesAutomatically = true;
            settings.MixedContentMode = MixedContentHandling.AlwaysAllow;
            settings.SetLayoutAlgorithm(WebSettings.LayoutAlgorithm.SingleColumn);
            
            var client = new WebViewClient();
            webView.SetWebViewClient(client);
        }

        public static void SetHtml([NotNull] this WebView webView, string data)
        {
            webView.LoadData(data, "text/html", "UTF-8");
        }
    }
}