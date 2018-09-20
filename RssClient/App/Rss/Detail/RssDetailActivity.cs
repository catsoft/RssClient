using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Newtonsoft.Json;
using RssClient.App.Base;
using System.Threading;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Shared.App.Base.Database;
using Shared.App.Rss;
using Shared.App.Rss.LoadMessages;

namespace RssClient.App.Rss.Detail
{
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
	public class RssDetailActivity : ToolbarActivity
	{
	    private RssModel _item;
	    private RecyclerView _list;
	    private SwipeRefreshLayout _refreshLayout;
	    protected override int ResourceView => Resource.Layout.activity_rss_detail;
        public const string ItemIntentId = "ItemIntentId";

	    protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

		    _item = JsonConvert.DeserializeObject<RssModel>(Intent.GetStringExtra(ItemIntentId));
		    Title = _item.Name;

		    _list = FindViewById<RecyclerView>(Resource.Id.rss_details_recycler_view);
            _list.SetLayoutManager(new LinearLayoutManager(this, LinearLayoutManager.Vertical, false));

		    _refreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.rss_details_refresher);
		    _refreshLayout.Refresh += (sender, args) => LoadItems();

            LoadItems();
        }

        private void LoadItems()
        {
            var thread = new Thread(() =>
            {
                var request = new LoadMessagesRequest(_item);
                var @delegate = this.GetCommandDelegate<LoadMessagesResponse>(OnSuccessLoad);
                var command = new LoadMessagesCommand(this, LocalDb.Instance, @delegate);
                command.Execute(request);
            });

            thread.Start();
        }

	    private void OnSuccessLoad(LoadMessagesResponse obj)
	    {
	        UpdateItems();
	    }

	    private void UpdateItems()
	    {
            RunOnUiThread(() =>
            {
                _refreshLayout.Refreshing = false;
                _item.LoadMessagesFromDb(LocalDb.Instance);

                var messages = _item.Messages;

                if (messages?.Any() == true)
                {
                    this.ShowValidData();
                }
                else
                {
                    this.ShowNotValidError("No data");
                }

                var adapter = new RssMessageAdapter(_item.Messages ?? new List<RssMessageModel>(), this);
                _list.SetAdapter(adapter);
            });
	    }
	}
}