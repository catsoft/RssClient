using System;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views.InputMethods;
using Android.Widget;
using RssClient.App.Base;

namespace RssClient.App.Rss.Create
{
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class RssCreateActivity : ToolbarActivity
    {
        private const string UrlDefault = "http://";
        private const string TitleActivity = "Create RSS";

        private TextInputLayout _name;
        private TextInputLayout _url;
        private Button _sendButton;

        protected override int ResourceView => Resource.Layout.activity_rss_create;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Title = TitleActivity;

            InitNameEditText();

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

        private void InitNameEditText()
        {
            _name = FindViewById<TextInputLayout>(Resource.Id.rss_create_name);
        }

        private void SendButtonOnClick(object sender, EventArgs eventArgs)
        {
            var name = _name.EditText.Text;
            var url = _url.EditText.Text;
			// TODO Воскресить создание android
            //var request = new NewRssRequest(name, url);

            //if (request.IsValid((field, error) => this.ShowFieldError(_fields, field, error)))
            //{
            //    var @delegate = this.GetCommandDelegate<NewRssResponse>(OnSuccessCreate);
            //    var command = new NewRssCommand(LocalDb.Instance, @delegate);

            //    command.Execute(request);
            //}
        }

        //private void OnSuccessCreate(NewRssResponse obj)
        //{
        //    SetResult(Result.Ok);
        //    Finish();
        //}
    }
}