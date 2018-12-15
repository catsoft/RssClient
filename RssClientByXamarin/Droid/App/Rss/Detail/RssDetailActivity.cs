﻿using Android.App;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Database.Rss;
using iOS.App.Rss.RssUpdater;
using RssClient.App.Base;
using RssClient.App.Rss.Edit;
using Shared.App.Rss;
using Shared.App.Rss.RssDatabase;

namespace RssClient.App.Rss.Detail
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class RssDetailActivity : ToolbarActivity
    {
        public const string ItemIntentId = "ItemIntentId";

        private RssMessagesRepository _rssMessagesRepository;
        private RssRepository _rssRepository;
        private RssUpdater _rssUpdater;

        private RssModel _item;
        private RecyclerView _list;
        private SwipeRefreshLayout _refreshLayout;

        protected override int ResourceView => Resource.Layout.activity_rss_detail;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _rssMessagesRepository = RssMessagesRepository.Instance;
            _rssRepository = RssRepository.Instance;
            _rssUpdater = RssUpdater.Instance;

            var idItem = Intent.GetStringExtra(ItemIntentId);
            _item = _rssRepository.Find(idItem);
            if (_item == null)
                return;

            Title = _item.Name;

            _list = FindViewById<RecyclerView>(Resource.Id.rss_details_recycler_view);
            _list.SetLayoutManager(new LinearLayoutManager(this, LinearLayoutManager.Vertical, false));

            _refreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.rss_details_refresher);
            _refreshLayout.Refresh += async (sender, args) =>
            {
                await _rssUpdater.StartUpdateAllByInternet(_item);
                _refreshLayout.Refreshing = false;
            };

            var items = _rssMessagesRepository.GetMessagesForRss(_item);
            var adapter = new RssMessageAdapter(items, this);
            _list.SetAdapter(adapter);
            adapter.NotifyDataSetChanged();

            //items.SubscribeForNotifications((sender, changes, error) =>
            //{
            //    adapter.NotifyDataSetChanged();
            //});

            await _rssUpdater.StartUpdateAllByInternet(_item);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            var inflater = MenuInflater;
            inflater.Inflate(Resource.Menu.rss_detail_menu, menu);

            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.rss_detail_menu_remove)
            {
                _rssRepository.Remove(_item);
                Finish();
            }
            else if (item.ItemId == Resource.Id.rss_detail_menu_edit)
            {
                var intent = RssEditActivity.Create(this, _item.Id);
                StartActivity(intent);
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}