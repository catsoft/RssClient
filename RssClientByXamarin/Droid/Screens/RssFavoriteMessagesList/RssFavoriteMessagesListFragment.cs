using Android.OS;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Android.Views;
using Droid.Container;
using Droid.Repository.Configuration;
using Droid.Screens.Base.SwipeButtonRecyclerView;
using Droid.Screens.Navigation;
using Droid.Screens.RssAllMessagesList;
using Shared.Configuration.Settings;
using Shared.Infrastructure.Navigation;
using Shared.Repository.RssMessage;
using Shared.ViewModels.RssFavoriteMessages;

namespace Droid.Screens.RssFavoriteMessagesList
{
    public class RssFavoriteMessagesListFragment : BaseFragment<RssFavoriteMessagesViewModel>
    {
        [Inject] private INavigator _navigator;

        [Inject] private IRssMessagesRepository _rssMessagesRepository;

        [Inject] private IConfigurationRepository _configurationRepository;

        protected override int LayoutId => Resource.Layout.fragment_favorite_messages_list;
        public override bool IsRoot => true;

        public RssFavoriteMessagesListFragment()
        {

        }

        protected override void RestoreState(Bundle saved)
        {

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            Title = Activity.GetText(Resource.String.rssFavoriteMessages_title);

            var appConfiguration = _configurationRepository.GetSettings<AppConfiguration>();

            var items = _rssMessagesRepository.GetFavoriteMessages();
            var recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView_favoriteMessages_list);
            recyclerView.SetLayoutManager(new LinearLayoutManager(Context, LinearLayoutManager.Vertical, false));
            recyclerView.AddItemDecoration(new DividerItemDecoration(Context, DividerItemDecoration.Vertical));
            var adapter = new RssAllMessagesListAdapter(items, Activity, _rssMessagesRepository, appConfiguration);
            recyclerView.SetAdapter(adapter);

            var callback = new SwipeButtonTouchHelperCallback();
            var helper = new ItemTouchHelper(callback);
            helper.AttachToRecyclerView(recyclerView);
            return view;
        }
    }
}