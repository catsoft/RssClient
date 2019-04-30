using Android.Graphics;
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
            webView.ScrollbarFadingEnabled = true;
            
            var settings = webView.Settings;
            settings.JavaScriptEnabled = true;
            settings.DomStorageEnabled = true;
            settings.SetAppCacheEnabled(true);
            settings.LoadsImagesAutomatically = true;
//            settings.LoadWithOverviewMode = true;
//            settings.UseWideViewPort = true;
            settings.MixedContentMode = MixedContentHandling.AlwaysAllow;
            
            var client = new WebViewClient();
            webView.SetWebViewClient(client);
            webView.SetBackgroundColor(Color.Transparent);
        }

        public static void InitZoom([NotNull] this WebView webView)
        {
            var settings = webView.Settings;
            settings.BuiltInZoomControls = true;
            settings.SetSupportZoom(true);
        }

        public static void DisableScroll([NotNull] this WebView webView)
        {
            SetScrollBar(webView, false);
        }

        public static void EnableScroll([NotNull] this WebView webView)
        {
            SetScrollBar(webView, true);
        }

        private static void SetScrollBar(WebView webView, bool value)
        {
            webView.HorizontalScrollBarEnabled = value;
            webView.VerticalScrollBarEnabled = value;
        }
        

        public static void SetHtml([NotNull] this WebView webView, string data)
        {
            webView.LoadData(data, "text/html", "UTF-8");
        }
    }
}