using System;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views.InputMethods;
using Android.Widget;
using Autofac;
using Repository;
using RssClient.Screens.Base;
using Shared;

namespace RssClient.Screens.Rss.Create
{
	[Activity(Label = "@string/all_appName", Theme = "@style/AppTheme.NoActionBar")]
    public class RssCreateActivity : ToolbarActivity
    {
        private TextInputLayout _url;
        private Button _sendButton;
	    private IRssRepository _rssRepository;

        protected override int ResourceView => Resource.Layout.activity_rss_create;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            _rssRepository = App.Container.Resolve<IRssRepository>();

			base.OnCreate(savedInstanceState);

            Title = GetText(Resource.String.create_titleActivity);

            InitUrlEditText();

            InitSendButton();
        }


        private void InitSendButton()
        {
            _sendButton = FindViewById<Button>(Resource.Id.button_rssCreate_submit);
            _sendButton.Click += SendButtonOnClick;
        }

        private void InitUrlEditText() 
        {
            _url = FindViewById<TextInputLayout>(Resource.Id.textInputLayout_rssCreate_link);
            _url.EditText.SetTextAndSetCursorToLast(GetText(Resource.String.create_urlDefault));
            _url.EditText.EditorAction += (sender, args) =>
            {
                if (args.ActionId == ImeAction.Done) _sendButton.CallOnClick();
            };
        }

        private async void SendButtonOnClick(object sender, EventArgs eventArgs)
        {
            var url = _url.EditText.Text;

			await _rssRepository.InsertByUrl(url);

			Finish();
		}
    }
}