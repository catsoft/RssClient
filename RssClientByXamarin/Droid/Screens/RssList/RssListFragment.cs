using System;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Autofac;
using Droid.Infrastructure;
using Droid.Screens.Navigation;
using Droid.Screens.RssAllMessagesList;
using Droid.Screens.RssCreate;
using Realms;
using RssClient.Repository;
using Shared;
using Shared.Services.Navigator;
using Shared.ViewModels;

namespace Droid.Screens.RssList
{
    public class RssListFragment : TitleFragment
    {
        private RecyclerView _recyclerView;
        private IRssRepository _rssRepository;
        private INavigator _navigator;

        public override bool RootFragment => true;

        public RssListFragment()
        {
            
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.menu_rssList, menu);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _rssRepository = App.Container.Resolve<IRssRepository>();
            _navigator = App.Container.Resolve<INavigator>();

            Title = Activity?.GetText(Resource.String.rssList_title);

            HasOptionsMenu = true;

            var view = inflater.Inflate(Resource.Layout.fragment_rss_list, container, false);

            var fab = view.FindViewById<FloatingActionButton>(Resource.Id.fab_rssList_addRss);
            fab.Click += FabOnClick;

            _recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView_rssList_list);
            _recyclerView.SetLayoutManager(new LinearLayoutManager(Context, LinearLayoutManager.Vertical, false));

            var items = _rssRepository.GetList();
            var adapter = new RssListAdapter(items, Activity);
            _recyclerView.SetAdapter(adapter);
            adapter.NotifyDataSetChanged();

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

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.menuItem_rssList_change)
            {
                _navigator.Go(App.Container.Resolve<IWay<RssAllMessagesViewModel, RssAllMessagesViewModel.Way.WayData>>());
            }
            
            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            RssCreateActivity.StartActivity(Activity);
        }
    }
}