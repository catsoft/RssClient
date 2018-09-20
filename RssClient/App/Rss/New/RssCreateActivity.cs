using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Widget;
using RssClient.App.Base;
using Shared.App.Base.Database;
using Shared.App.Rss.New;
using Shared.App.Rss.New.NewCommand;

namespace RssClient.App.Rss.New
{
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
	public class RssCreateActivity : ToolbarActivity
	{
	    private TextInputLayout _name;
	    private TextInputLayout _url;
	    private Button _sendButton;
	    private Dictionary<NewRssField, TextInputLayout> _fields;


	    protected override int ResourceView => Resource.Layout.activity_rss_create;

        protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

		    Title = "Создание RSS";

		    _name = FindViewById<TextInputLayout>(Resource.Id.rss_create_name);
            _url = FindViewById<TextInputLayout>(Resource.Id.rss_create_rss);

            _sendButton = FindViewById<Button>(Resource.Id.rss_create_button);
            _sendButton.Click += SendButtonOnClick;

		    _fields = new Dictionary<NewRssField, TextInputLayout>()
		    {
		        {NewRssField.Name, _name},
		        {NewRssField.Rss, _url},
		    };
        }

	    private void SendButtonOnClick(object sender, EventArgs eventArgs)
	    {
	        var name = _name.EditText.Text;
	        var url = _url.EditText.Text;

	        var request = new NewRssRequest(name, url);

	        if (request.IsValid((field, error) => this.ShowFieldError(_fields, field, error)))
	        {
	            var @delegate = this.GetCommandDelegate<NewRssResponse>(OnSuccessCreate);
                var command = new NewRssCommand(this, LocalDb.Instance, @delegate);
                
                command.Execute(request);
	        }
	    }

	    private void OnSuccessCreate(NewRssResponse obj)
	    {
            RunOnUiThread(() =>
            {
                SetResult(Result.Ok);
                RunOnUiThread(Finish);
            });
	    }
	}
}