using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using RssClient.App.Base;
using RssClient.App.Rss.Create;
using Shared.App.Base.Database;
using Shared.App.Rss;

namespace RssClient.App.Rss.List
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class RssListActivity : ToolbarActivity
    {
        public const int EditRequestCode = 98;
        private const int CreateResultCode = 99;
        private const string TitleActivity = "RSS client";

        private RecyclerView _recyclerView;
        private List<RssModel> _items;

        protected override int ResourceView => Resource.Layout.activity_rss_list;
        protected override bool IsDisplayHomeAsUpEnable => false;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Title = TitleActivity;

            var fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            _recyclerView = FindViewById<RecyclerView>(Resource.Id.rss_list_recycler_view);
            _recyclerView.SetLayoutManager(new LinearLayoutManager(this, LinearLayoutManager.Vertical, false));

            LoadItems();
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok && (requestCode == CreateResultCode || requestCode == EditRequestCode))
                LoadItems();
        }


        private void LoadItems()
        {
            _items = LocalDb.Instance.GetItems<RssModel>().ToList();
            var adapter = new RssListAdapter(_items, this);
            _recyclerView.SetAdapter(adapter);
            adapter.NotifyDataSetChanged();
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            var intent = new Intent(this, typeof(RssCreateActivity));
            StartActivityForResult(intent, CreateResultCode);
        }
    }
}