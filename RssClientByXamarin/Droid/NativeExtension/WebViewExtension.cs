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
            settings.MixedContentMode = MixedContentHandling.AlwaysAllow;
            
            var client = new NotRedirectWebViewClient();
            webView.SetWebViewClient(client);
            webView.SetBackgroundColor(Color.Transparent);
        }
        
        private class NotRedirectWebViewClient : WebViewClient
        {
            public override bool ShouldOverrideUrlLoading(WebView view, IWebResourceRequest request)
            {
                var url = request.Url.ToString();
                Xamarin.Essentials.Launcher.OpenAsync(url);
                return true;
            }
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
        
        public static void TurnLoadImages([NotNull] this WebView webView, bool isShowImage)
        {
            var settings = webView.Settings;
            settings.DomStorageEnabled = isShowImage;
            settings.SetAppCacheEnabled(isShowImage);
            settings.LoadsImagesAutomatically = isShowImage;
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