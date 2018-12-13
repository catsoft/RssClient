using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using iOS.App.Rss.RssUpdater;
using Realms;
using RssClient.App.Base;
using RssClient.App.Rss.Create;
using Shared.App.Rss;

namespace RssClient.App.Rss.List
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class RssListActivity : ToolbarActivity
    {
        public const int EditRequestCode = 98;
        private const int CreateResultCode = 99;
        private const string TitleActivity = "RSS client";

        private RecyclerView _recyclerView;
	    private RssRepository _rssRepository;
	    private RssUpdater _rssUpdater;

        protected override int ResourceView => Resource.Layout.activity_rss_list;
        protected override bool IsDisplayHomeAsUpEnable => false;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

	        _rssRepository = RssRepository.Instance;
	        _rssUpdater = RssUpdater.Instance;

			Title = TitleActivity;

            var fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            _recyclerView = FindViewById<RecyclerView>(Resource.Id.rss_list_recycler_view);
            _recyclerView.SetLayoutManager(new LinearLayoutManager(this, LinearLayoutManager.Vertical, false));

	        var items = _rssRepository.GetList();
			var adapter = new RssListAdapter(items, this);
			_recyclerView.SetAdapter(adapter);
			adapter.NotifyDataSetChanged();

	        items.SubscribeForNotifications((sender, changes, error) =>
	        {
		        adapter.NotifyDataSetChanged();
			});

	        await _rssUpdater.StartUpdateAllByInternet();
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            var intent = new Intent(this, typeof(RssCreateActivity));
            StartActivityForResult(intent, CreateResultCode);
        }
    }
}