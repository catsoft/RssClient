using System.Linq;
using Android.App;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Autofac;
using Droid.Screens.Base;
using Droid.Screens.Rss.RssEdit;
using RssClient.Repository;
using Shared;
using Shared.Database.Rss;
using Shared.Repository;
using Shared.Services.Navigator;
using Fragment = Android.Support.V4.App.Fragment;

namespace Droid.Screens.Rss.RssItemDetail
{
    [Activity(Label = "@string/all_appName", Theme = "@style/AppTheme.NoActionBar")]
    public class RssDetailItemFragment : Fragment, ITitle
    {
        public const string ItemIntentId = "ItemIntentId";

        private IRssMessagesRepository _rssMessagesRepository;
        private IRssRepository _rssRepository;
        private INavigator _navigator;

        private RssModel _item;
        private RecyclerView _list;
        private SwipeRefreshLayout _refreshLayout;

        private string _title;
        public string Title => _title;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.activity_rss_detail, container, false);

            _rssMessagesRepository = App.Container.Resolve<IRssMessagesRepository>();
            _rssRepository = App.Container.Resolve<IRssRepository>();
            _navigator = App.Container.Resolve<INavigator>();

            var idItem = savedInstanceState.GetString(ItemIntentId);
            _item = _rssRepository.Find(idItem);

            _title = _item.Name;

            _list = view.FindViewById<RecyclerView>(Resource.Id.recyclerView_rssDetail_messageList);
            _list.SetLayoutManager(new LinearLayoutManager(Activity, LinearLayoutManager.Vertical, false));

            _refreshLayout = view.FindViewById<SwipeRefreshLayout>(Resource.Id.swipeRefreshLayout_rssDetail_refresher);
            _refreshLayout.Refresh += async (sender, args) =>
            {
                await _rssRepository.StartUpdateAllByInternet(_item.Rss, _item.Id);
                _refreshLayout.Refreshing = false;
            };

            var items = _rssMessagesRepository.GetMessagesForRss(_item);
            var adapter = new RssMessageAdapter(items.ToList(), Activity);
            _list.SetAdapter(adapter);
            adapter.NotifyDataSetChanged();

            _item.PropertyChanged += (sender, args) =>
            {
                adapter.Items.Clear();
                var newItems = _rssMessagesRepository.GetMessagesForRss(_item);
                adapter.Items.AddRange(newItems);
                adapter.NotifyDataSetChanged();
            };

            _rssRepository.StartUpdateAllByInternet(_item.Rss, _item.Id);

            return view;
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.menu_rssDetail, menu);
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
            var intent = RssEditActivity.StartActivity(Activity, holderItem.Id);
            StartActivity(intent);
        }

        private void DeleteItem(RssModel holderItem)
        {
            var builder = new AlertDialog.Builder(Activity);
            builder.SetPositiveButton(GetText(Resource.String.rssDeleteDialog_positiveTitle), (sender, args) =>
            {
                _rssRepository.Remove(holderItem);
                Activity.OnBackPressed();
            });
            builder.SetNegativeButton(GetText(Resource.String.rssDeleteDialog_negativeTitle), (sender, args) => { });
            builder.SetTitle(GetText(Resource.String.rssDeleteDialog_Title));
            builder.Show();
        }
    }
}