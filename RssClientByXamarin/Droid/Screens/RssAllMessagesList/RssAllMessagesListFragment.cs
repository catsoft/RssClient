using System;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Autofac;
using Droid.Container;
using Droid.Infrastructure;
using Droid.Screens.Navigation;
using Droid.Screens.RssCreate;
using Shared;
using Shared.Repository;
using Shared.Services.Navigator;
using Shared.ViewModels;

namespace Droid.Screens.RssAllMessagesList
{
    public class RssAllMessagesListFragment : TitleFragment
    {
        [Inject]
        private INavigator _navigator;
        
        [Inject]
        private IRssMessagesRepository _rssMessagesRepository;

        protected override int LayoutId => Resource.Layout.fragment_all_messages_list;
        public override bool RootFragment => true;
        
        public RssAllMessagesListFragment()
        {
            
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.menu_rssList, menu);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Title = Activity.GetText(Resource.String.rssList_title);
            
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            HasOptionsMenu = true;

            var items = _rssMessagesRepository.GetAllMessages();
            var recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView_allMessages_list);
            recyclerView.SetLayoutManager(new LinearLayoutManager(Context, LinearLayoutManager.Vertical, false));
            var adapter = new RssAllMessagesListAdapter(items, Activity);
            recyclerView.SetAdapter(adapter);

            var fab = view.FindViewById<FloatingActionButton>(Resource.Id.fab_allMessages_addRss);
            fab.Click += OnFabClick;

            return view;
        }
        
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.menuItem_rssList_change)
            {
                _navigator.Go(App.Container.Resolve<IWay<RssListViewModel, RssListViewModel.Way.WayData>>());
            }
            
            return base.OnOptionsItemSelected(item);
        }

        private void OnFabClick(object sender, EventArgs e)
        {
            var createWay = App.Container.Resolve<IWay<RssCreateViewModel,RssCreateViewModel.Way.WayData>>();
            _navigator.Go(createWay);
        }
    }
}