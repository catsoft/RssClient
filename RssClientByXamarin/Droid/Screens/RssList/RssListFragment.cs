using System;
using System.Linq;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Android.Views;
using Autofac;
using Droid.Container;
using Droid.Repository.Configuration;
using Droid.Screens.Base.SwipeRecyclerView;
using Droid.Screens.Navigation;
using Shared;
using Shared.Configuration.Settings;
using Shared.Repository.Rss;
using Shared.Services.Navigator;
using Shared.ViewModels;

namespace Droid.Screens.RssList
{
    public class RssListFragment : TitleFragment
    {
        [Inject] private IRssRepository _rssRepository;

        [Inject] private INavigator _navigator;

        [Inject] private IConfigurationRepository _configurationRepository;

        protected override int LayoutId => Resource.Layout.fragment_rss_list;
        public override bool RootFragment => true;

        public RssListFragment()
        {

        }

        protected override void RestoreState(Bundle saved)
        {

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            
            Title = Activity?.GetText(Resource.String.rssList_title);

            HasOptionsMenu = true;

            var appConfiguration = _configurationRepository.GetSettings<AppConfiguration>();
            
            var fab = view.FindViewById<FloatingActionButton>(Resource.Id.fab_rssList_addRss);
            fab.Click += FabOnClick;

            var recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView_rssList_list);
            recyclerView.SetLayoutManager(new LinearLayoutManager(Context, LinearLayoutManager.Vertical, false));

            var items = _rssRepository.GetList();
            var adapter = new RssListAdapter(items.ToList(), Activity, appConfiguration);
            recyclerView.SetAdapter(adapter);
            adapter.NotifyDataSetChanged();

            var callback = new SwipeTouchHelperCallback(adapter);
            var touchHelper = new ItemTouchHelper(callback);
            touchHelper.AttachToRecyclerView(recyclerView);
            
            return view;
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.menu_rssList, menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menuItem_rssList_change:
                    _navigator.Go(App.Container.Resolve<RssAllMessagesViewModel.Way>());
                    break;
                
                case Resource.Id.menuItem_rssList_editMode:
                    _navigator.Go(App.Container.Resolve<RssListEditViewModel.Way>());
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            var createWay = App.Container.Resolve<RssCreateViewModel.Way>();
            _navigator.Go(createWay);
        }
    }
}