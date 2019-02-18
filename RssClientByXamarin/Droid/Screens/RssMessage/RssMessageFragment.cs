using Android.OS;
using Android.Views;
using Android.Webkit;
using Droid.Container;
using Droid.Screens.Navigation;
using Shared.Repository.RssMessage;

namespace Droid.Screens.RssMessage
{
    public class RssMessageFragment : TitleFragment
    {
        [Inject] private IRssMessagesRepository _rssMessagesRepository;

        private string _rssMessageId;

        protected override int LayoutId => Resource.Layout.fragment_rss_message;
        public override bool RootFragment => false;

        public RssMessageFragment()
        {

        }

        public RssMessageFragment(string rssMessageId)
        {
            _rssMessageId = rssMessageId;
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);

            outState.PutString(nameof(_rssMessageId), _rssMessageId);
        }

        protected override void RestoreState(Bundle saved)
        {
            _rssMessageId = saved.GetString(nameof(_rssMessageId));
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var message = _rssMessagesRepository.FindById(_rssMessageId);

            Title = message.Title;

            var webView = view.FindViewById<WebView>(Resource.Id.webView_rssMessage_mainView);

            webView.ScrollBarStyle = ScrollbarStyles.OutsideOverlay;
            webView.ScrollbarFadingEnabled = false;

            var settings = webView.Settings;
            settings.JavaScriptEnabled = true;
            settings.BuiltInZoomControls = true;
            settings.SetSupportZoom(true);

            var client = new WebViewClient();
            webView.SetWebViewClient(client);
            webView.LoadUrl(message.Url);

            return view;
        }
    }
}