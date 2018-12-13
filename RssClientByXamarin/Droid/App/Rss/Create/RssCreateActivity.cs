using System;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views.InputMethods;
using Android.Widget;
using RssClient.App.Base;
using Shared.App.Rss;

namespace RssClient.App.Rss.Create
{
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class RssCreateActivity : ToolbarActivity
    {
        private const string UrlDefault = "http://";
        private const string TitleActivity = "Create RSS";

        private TextInputLayout _url;
        private Button _sendButton;
	    private RssRepository _rssRepository;

        protected override int ResourceView => Resource.Layout.activity_rss_create;

        protected override void OnCreate(Bundle savedInstanceState)
        {
	        _rssRepository = RssRepository.Instance;

			base.OnCreate(savedInstanceState);

            Title = TitleActivity;

            InitUrlEditText();

            InitSendButton();
        }


        private void InitSendButton()
        {
            _sendButton = FindViewById<Button>(Resource.Id.rss_create_button);
            _sendButton.Click += SendButtonOnClick;
        }

        private void InitUrlEditText() 
        {
            _url = FindViewById<TextInputLayout>(Resource.Id.rss_create_rss);
            _url.EditText.SetTextAndSetCursorToLast(UrlDefault);
            _url.EditText.EditorAction += (sender, args) =>
            {
                if (args.ActionId == ImeAction.Done) _sendButton.CallOnClick();
            };
        }

        private void SendButtonOnClick(object sender, EventArgs eventArgs)
        {
            var url = _url.EditText.Text;

			_rssRepository.InsertByUrl(url);

			Finish();
		}
    }
}