using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views.InputMethods;
using Android.Widget;
using Autofac;
using Database.Rss;
using Repository;
using RssClient.Screens.Base;

namespace RssClient.Screens.Rss.Edit
{
    [Activity(Label = "@string/all_appName", Theme = "@style/AppTheme.NoActionBar")]
    public class RssEditActivity : ToolbarActivity
    {
        public const string ItemIntentId = "ItemIntentId";

        private TextInputLayout _name;
        private TextInputLayout _url;
        private Button _sendButton;
        private RssModel _item;

	    private IRssRepository _rssRepository;

        protected override int ResourceView => Resource.Layout.activity_rss_edit;

        public static Intent Create(Context context, string rssId)
        {
            var intent = new Intent(context, typeof(RssEditActivity));
            intent.PutExtra(RssEditActivity.ItemIntentId, rssId);

            return intent;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

	        _rssRepository = Shared.App.Container.Resolve<IRssRepository>();

			Title = GetText(Resource.String.edit_titleActivity);

            var idItem = Intent.GetStringExtra(ItemIntentId);
	        _item = _rssRepository.Find(idItem);

            if (_item == null)
                return;

            InitNameEditText();

            InitUrlEditText();

            InitSendButton();
        }

        private void InitSendButton()
        {
            _sendButton = FindViewById<Button>(Resource.Id.button_rssEdit_submit);
            _sendButton.Click += SendButtonOnClick;
        }

        private void InitUrlEditText()
        {
            _url = FindViewById<TextInputLayout>(Resource.Id.textInputLayout_rssEdit_link);
            _url.EditText.SetTextAndSetCursorToLast(_item.Rss);
            _url.EditText.EditorAction += (sender, args) =>
            {
                if (args.ActionId == ImeAction.Done) _sendButton.CallOnClick();
            };
        }

        private void InitNameEditText()
        {
            _name = FindViewById<TextInputLayout>(Resource.Id.textInputLayout_rssEdit_name);
            _name.EditText.SetTextAndSetCursorToLast(_item.Name);
        }

        private async void SendButtonOnClick(object sender, EventArgs eventArgs)
        {
            var name = _name.EditText.Text;
            var url = _url.EditText.Text;

	        await _rssRepository.Update(_item.Id, url, name);

			Finish();
        }
    }
}