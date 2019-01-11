using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Autofac;
using Droid.Screens.Base;
using Droid.Screens.Rss.Create;
using Realms;
using RssClient;
using RssClient.Repository;
using Shared;

namespace Droid.Screens.Rss.List
{
    [Activity(Label = "@string/all_appName", Theme = "@style/AppTheme.NoActionBar")]
    public class RssListActivity : ToolbarActivity
    {
        public const int EditRequestCode = 98;
        private const int CreateResultCode = 99;

        private RecyclerView _recyclerView;
	    private IRssRepository _rssRepository;

        protected override int ResourceView => Resource.Layout.activity_rss_list;
        protected override bool IsDisplayHomeAsUpEnable => false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

	        _rssRepository = App.Container.Resolve<IRssRepository>();

			Title = GetText(Resource.String.rssList_titleActivity);

            var fab = FindViewById<FloatingActionButton>(Resource.Id.fab_rssList_addRss);
            fab.Click += FabOnClick;

            _recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView_rssList_list);
            _recyclerView.SetLayoutManager(new LinearLayoutManager(this, LinearLayoutManager.Vertical, false));

	        var items = _rssRepository.GetList();
			var adapter = new RssListAdapter(items, this);
			_recyclerView.SetAdapter(adapter);
			adapter.NotifyDataSetChanged();

	        items.SubscribeForNotifications((sender, changes, error) =>
	        {
                if (sender != null && changes != null)
                {
                    foreach (var changesInsertedIndex in changes.InsertedIndices)
                    {
                        adapter.NotifyItemInserted(changesInsertedIndex);
                    }

                    foreach (var changesInsertedIndex in changes.ModifiedIndices)
                    {
                        adapter.NotifyItemChanged(changesInsertedIndex);
                    }

                    foreach (var changesInsertedIndex in changes.DeletedIndices)
                    {
                        adapter.NotifyItemRemoved(changesInsertedIndex);
                    }
                }

                adapter.NotifyDataSetChanged();
			});
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            var intent = new Intent(this, typeof(RssCreateActivity));
            StartActivityForResult(intent, CreateResultCode);
        }
    }
}