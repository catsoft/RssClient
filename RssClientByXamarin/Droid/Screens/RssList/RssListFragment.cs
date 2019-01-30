using System;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Android.Views;
using Autofac;
using Droid.Container;
using Droid.Screens.Base.SwipeRecyclerView;
using Droid.Screens.Navigation;
using Realms;
using RssClient.Repository;
using Shared;
using Shared.Services.Navigator;
using Shared.ViewModels;

namespace Droid.Screens.RssList
{
    public class RssListFragment : TitleFragment
    {
        [Inject]
        private IRssRepository _rssRepository;
        
        [Inject]
        private INavigator _navigator;

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
            Title = Activity?.GetText(Resource.String.rssList_title);

            HasOptionsMenu = true;

            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var fab = view.FindViewById<FloatingActionButton>(Resource.Id.fab_rssList_addRss);
            fab.Click += FabOnClick;

            var recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView_rssList_list);
            recyclerView.SetLayoutManager(new LinearLayoutManager(Context, LinearLayoutManager.Vertical, false));

            var items = _rssRepository.GetList();
            var adapter = new RssListAdapter(items, Activity);
            recyclerView.SetAdapter(adapter);
            adapter.NotifyDataSetChanged();

            var callback = new SwipeTouchHelperCallback(adapter);
            var touchHelper = new ItemTouchHelper(callback);
            touchHelper.AttachToRecyclerView(recyclerView);
            
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

            return view;
        }
        
        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.menu_rssList, menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.menuItem_rssList_change)
            {
                _navigator.Go(App.Container.Resolve<IWay<RssAllMessagesViewModel.Way.WayData>>());
            }
            
            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            var createWay = App.Container.Resolve<IWay<RssCreateViewModel.Way.WayData>>();
            _navigator.Go(createWay);
        }
    }
}