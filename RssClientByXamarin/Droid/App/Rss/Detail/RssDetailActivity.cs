using Android.App;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Database.Rss;
using Newtonsoft.Json;
using RssClient.App.Base;

namespace RssClient.App.Rss.Detail
{
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class RssDetailActivity : ShimmerActivity
    {
        public const string ItemIntentId = "ItemIntentId";

        private RssModel _item;
        private RecyclerView _list;
        private SwipeRefreshLayout _refreshLayout;

        protected override int ResourceView => Resource.Layout.activity_rss_detail;

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
			// TODO прикрутить загрузку элементов
            //var asyncTask = new GetMessagesTask(_list, this, _refreshLayout);
            //asyncTask.Execute(_item);
        }
    }
}