﻿using System;
using System.Linq;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Android.Views;
using Autofac;
using Droid.Container;
using Droid.NativeExtension;
using Droid.Screens.Base.SwipeButtonRecyclerView;
using Droid.Screens.Navigation;
using Shared;
using Shared.Repository;
using Shared.Services.Navigator;
using Shared.ViewModels;

namespace Droid.Screens.RssAllMessagesList
{
    public class RssAllMessagesListFragment : TitleFragment
    {
        [Inject] private INavigator _navigator;

        [Inject] private IRssMessagesRepository _rssMessagesRepository;

        protected override int LayoutId => Resource.Layout.fragment_all_messages_list;
        public override bool RootFragment => true;

        public RssAllMessagesListFragment()
        {

        }

        protected override void RestoreState(Bundle saved)
        {

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            Title = Activity.GetText(Resource.String.rssList_title);

            HasOptionsMenu = true;

            var items = _rssMessagesRepository.GetAllMessages();
            var recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView_allMessages_list);
            recyclerView.SetLayoutManager(new LinearLayoutManager(Context, LinearLayoutManager.Vertical, false));
            recyclerView.AddItemDecoration(new DividerItemDecoration(Context, DividerItemDecoration.Vertical));
            var adapter = new RssAllMessagesListAdapter(items, Activity, _rssMessagesRepository);
            recyclerView.SetAdapter(adapter);

            var fab = view.FindViewById<FloatingActionButton>(Resource.Id.fab_allMessages_addRss);
            fab.Click += OnFabClick;

            var callback = new SwipeButtonTouchHelperCallback();
            var helper = new ItemTouchHelper(callback);
            helper.AttachToRecyclerView(recyclerView);

            return view;
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.menu_rssAllMessageList, menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.menuItem_rssAllMessageList_change)
            {
                _navigator.Go(App.Container.Resolve<RssListViewModel.Way>());
            }

            return base.OnOptionsItemSelected(item);
        }

        private void OnFabClick(object sender, EventArgs e)
        {
            var createWay = App.Container.Resolve<RssCreateViewModel.Way>();
            _navigator.Go(createWay);
        }
    }
}