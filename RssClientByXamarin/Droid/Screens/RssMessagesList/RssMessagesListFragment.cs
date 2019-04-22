using Android.App;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Android.Views;
using Autofac;
using Droid.Container;
using Droid.Repositories.Configuration;
using Droid.Screens.Base.SwipeButtonRecyclerView;
using Droid.Screens.Navigation;
using Shared;
using Shared.Configuration.Settings;
using Shared.Infrastructure.Navigation;
using Shared.Repositories.Rss;
using Shared.Repositories.RssMessage;
using Shared.Services.Rss;
using Shared.ViewModels.RssEdit;
using Shared.ViewModels.RssItemDetail;
using Xamarin.Essentials;

namespace Droid.Screens.RssMessagesList
{
    public class RssMessagesListFragment : BaseFragment<RssMessagesListViewModel>
    {
        [Inject] private IConfigurationRepository _configurationRepository;
        private string _itemId;

        [Inject] private INavigator _navigator;

        [Inject] private IRssMessagesRepository _rssMessagesRepository;

        [Inject] private IRssRepository _rssRepository;

        [Inject] private IRssService _rssService;

        public RssMessagesListFragment() { }

        public RssMessagesListFragment(string itemId) { _itemId = itemId; }

        private RssDomainModel Item => _rssRepository.GetAsync(_itemId).Result;

        protected override int LayoutId => Resource.Layout.fragment_rss_detail;
        public override bool IsRoot => false;

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);

            outState.PutString(nameof(_itemId), _itemId);
        }

        protected override void RestoreState(Bundle saved) { _itemId = saved.GetString(nameof(_itemId)); }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            HasOptionsMenu = true;

            var item = Item;
            Title = item.Name;

            var appConfiguration = _configurationRepository.GetSettings<AppConfiguration>();

            var list = view.FindViewById<RecyclerView>(Resource.Id.recyclerView_rssDetail_messageList);
            list.SetLayoutManager(new LinearLayoutManager(Activity, LinearLayoutManager.Vertical, false));
            list.AddItemDecoration(new DividerItemDecoration(Context, DividerItemDecoration.Vertical));

            var refreshLayout = view.FindViewById<SwipeRefreshLayout>(Resource.Id.swipeRefreshLayout_rssDetail_refresher);
            refreshLayout.Refresh += async (sender, args) =>
            {
                await _rssService.LoadAndUpdateAsync(item.Rss);
                refreshLayout.Refreshing = false;
            };

            var items = _rssMessagesRepository.GetMessagesForRss(item.Id);
            var adapter = new RssMessagesListAdapter(Activity, appConfiguration);
            list.SetAdapter(adapter);
            adapter.NotifyDataSetChanged();

            var callback = new SwipeButtonTouchHelperCallback();
            var touchHelper = new ItemTouchHelper(callback);
            touchHelper.AttachToRecyclerView(list);

            return view;
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater) { inflater.Inflate(Resource.Menu.menu_rssDetail, menu); }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menuItem_rssDetail_remove:
                    DeleteItem();
                    break;
                case Resource.Id.menuItem_rssDetail_edit:
                    EditItem();
                    break;
                case Resource.Id.menuItem_rssDetail_share:
                    ShareItem();
                    break;
                case Resource.Id.menuItem_rssDetail_readAllMessages:
                    ReadAllMessages();
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void ReadAllMessages() { _rssService.ReadAllMessagesAsync(Item.Id); }

        private async void ShareItem() { await Share.RequestAsync(Item.Rss); }

        private void EditItem()
        {
            var navigator = App.Container.Resolve<INavigator>();
            var parameter = new RssEditParameters(_itemId);
            var typedParameter = new TypedParameter(parameter.GetType(), parameter);
            var editWay = App.Container.Resolve<IWayWithParameters<RssEditViewModel, RssEditParameters>>(typedParameter);
            navigator.Go(editWay);
        }

        private void DeleteItem()
        {
            var builder = new AlertDialog.Builder(Activity);
            builder.SetPositiveButton(GetText(Resource.String.rssDeleteDialog_positiveTitle),
                (sender, args) =>
                {
                    _rssRepository.RemoveAsync(Item.Id);
                    _navigator.GoBack();
                });
            builder.SetNegativeButton(GetText(Resource.String.rssDeleteDialog_negativeTitle), (sender, args) => { });
            builder.SetTitle(GetText(Resource.String.rssDeleteDialog_Title));
            builder.Show();
        }
    }
}
