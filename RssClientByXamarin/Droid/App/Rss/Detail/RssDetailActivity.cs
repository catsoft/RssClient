using System.Linq;
using Android.App;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Database.Rss;
using Repository;
using RssClient.App.Base;
using RssClient.App.Rss.Edit;
using RssClient.App.Rss.List;

namespace RssClient.App.Rss.Detail
{
    [Activity(Label = "@string/all_appName", Theme = "@style/AppTheme.NoActionBar")]
    public class RssDetailActivity : ToolbarActivity
    {
        public const string ItemIntentId = "ItemIntentId";

        private RssMessagesRepository _rssMessagesRepository;
        private RssRepository _rssRepository;

        private RssModel _item;
        private RecyclerView _list;
        private SwipeRefreshLayout _refreshLayout;

        protected override int ResourceView => Resource.Layout.activity_rss_detail;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _rssMessagesRepository = RssMessagesRepository.Instance;
            _rssRepository = RssRepository.Instance;

            var idItem = Intent.GetStringExtra(ItemIntentId);
            _item = _rssRepository.Find(idItem);
            if (_item == null)
                return;

            Title = _item.Name;

            _list = FindViewById<RecyclerView>(Resource.Id.recyclerView_rssDetail_messageList);
            _list.SetLayoutManager(new LinearLayoutManager(this, LinearLayoutManager.Vertical, false));

            _refreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.swipeRefreshLayout_rssDetail_refresher);
            _refreshLayout.Refresh += async (sender, args) =>
            {
                await _rssRepository.StartUpdateAllByInternet(_item.Rss, _item.Id);
                _refreshLayout.Refreshing = false;
            };

            var items = _rssMessagesRepository.GetMessagesForRss(_item);
            var adapter = new RssMessageAdapter(items.ToList(), this);
            _list.SetAdapter(adapter);
            adapter.NotifyDataSetChanged();

            _item.PropertyChanged += (sender, args) =>
            {
                adapter.Items.Clear();
                var newItems = _rssMessagesRepository.GetMessagesForRss(_item);
                adapter.Items.AddRange(newItems);
                adapter.NotifyDataSetChanged();
            };

            await _rssRepository.StartUpdateAllByInternet(_item.Rss, _item.Id);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            var inflater = MenuInflater;
            inflater.Inflate(Resource.Menu.menu_rssDetail, menu);

            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.menuItem_rssDetail_remove)
            {
                DeleteItem(_item);
            }
            else if (item.ItemId == Resource.Id.menuItem_rssDetail_edit)
            {
                EditItem(_item);
            }

            return base.OnOptionsItemSelected(item);
        }

        private void EditItem(RssModel holderItem)
        {
            var intent = RssEditActivity.Create(this, holderItem.Id);
            StartActivityForResult(intent, RssListActivity.EditRequestCode);
        }

        private void DeleteItem(RssModel holderItem)
        {
            var builder = new AlertDialog.Builder(this);
            builder.SetPositiveButton(GetText(Resource.String.rssDeleteDialog_positiveTitle), (sender, args) =>
            {
                _rssRepository.Remove(holderItem);
                Finish();
            });
            builder.SetNegativeButton(GetText(Resource.String.rssDeleteDialog_negativeTitle), (sender, args) => { });
            builder.SetTitle(GetText(Resource.String.rssDeleteDialog_Title));
            builder.Show();
        }
    }
}