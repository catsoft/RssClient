using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views.InputMethods;
using Android.Widget;
using Newtonsoft.Json;
using RssClient.App.Base;
using Shared.App.Base.Database;
using Shared.App.Rss;
using Shared.App.Rss.Edit;
using Shared.App.Rss.New;

namespace RssClient.App.Rss.Edit
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class RssEditActivity : ToolbarActivity
    {
        public const string ItemIntentId = "ItemIntentId";
        private const string TitleActivity = "Edit RSS";

        private TextInputLayout _name;
        private TextInputLayout _url;
        private Button _sendButton;
        private Dictionary<NewRssField, TextInputLayout> _fields;
        private RssModel _item;

        protected override int ResourceView => Resource.Layout.activity_rss_edit;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Title = TitleActivity;

            var stringItem = Intent.GetStringExtra(ItemIntentId);
            _item = JsonConvert.DeserializeObject<RssModel>(stringItem);
            if (_item == null)
                return;

            InitNameEditText();

            InitUrlEditText();

            InitSendButton();

            _fields = new Dictionary<NewRssField, TextInputLayout>
            {
                {NewRssField.Name, _name},
                {NewRssField.Rss, _url}
            };
        }


        private void InitSendButton()
        {
            _sendButton = FindViewById<Button>(Resource.Id.rss_edit_button);
            _sendButton.Click += SendButtonOnClick;
        }

        private void InitUrlEditText()
        {
            _url = FindViewById<TextInputLayout>(Resource.Id.rss_edit_rss);
            _url.EditText.SetTextAndSetCursorToLast(_item.Rss);
            _url.EditText.EditorAction += (sender, args) =>
            {
                if (args.ActionId == ImeAction.Done) _sendButton.CallOnClick();
            };
        }

        private void InitNameEditText()
        {
            _name = FindViewById<TextInputLayout>(Resource.Id.rss_edit_name);
            _name.EditText.SetTextAndSetCursorToLast(_item.Name);
        }

        private void SendButtonOnClick(object sender, EventArgs eventArgs)
        {
            var name = _name.EditText.Text;
            var url = _url.EditText.Text;

            var request = new EditRssRequest(_item, name, url);

            if (request.IsValid((field, error) => this.ShowFieldError(_fields, field, error)))
            {
                var @delegate = this.GetCommandDelegate<EditRssResponse>(OnSuccessEdit);
                var command = new EditRssCommand(LocalDb.Instance, @delegate);

                command.Execute(request);
            }
        }

        private void OnSuccessEdit(EditRssResponse obj)
        {
            SetResult(Result.Ok);
            Finish();
        }
    }
}