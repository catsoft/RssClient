using Android.OS;
using Android.Views;
using Droid.Container;
using Droid.Screens.Navigation;
using Shared.Repositories.RssMessage;
using Shared.ViewModels.RssMessage;

namespace Droid.Screens.RssMessage
{
    public class RssMessageFragment : BaseFragment<RssMessageViewModel>
    {
        private string _rssMessageId;
        [Inject] private IRssMessagesRepository _rssMessagesRepository;

        public RssMessageFragment() { }

        public RssMessageFragment(string rssMessageId) { _rssMessageId = rssMessageId; }

        protected override int LayoutId => Resource.Layout.fragment_rss_message;
        public override bool IsRoot => false;

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);

            outState.PutString(nameof(_rssMessageId), _rssMessageId);
        }

        protected override void RestoreState(Bundle saved) { _rssMessageId = saved.GetString(nameof(_rssMessageId)); }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

//            var message = _rssMessagesRepository.GetAsync(_rssMessageId);
//
//            Title = message.Title;
//
//            var webView = view.FindViewById<WebView>(Resource.Id.webView_rssMessage_mainView);
//
//            webView.ScrollBarStyle = ScrollbarStyles.OutsideOverlay;
//            webView.ScrollbarFadingEnabled = false;
//
//            var settings = webView.Settings;
//            settings.JavaScriptEnabled = true;
//            settings.BuiltInZoomControls = true;
//            settings.SetSupportZoom(true);
//
//            var client = new WebViewClient();
//            webView.SetWebViewClient(client);
//            webView.LoadUrl(message.Url);

            return view;
        }
    }
}
