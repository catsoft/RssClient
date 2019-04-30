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
            WebView.Init();
            WebView.InitZoom();
            WebView.EnableScroll();
        }
        
        [NotNull] public WebView WebView { get; }
    }
}