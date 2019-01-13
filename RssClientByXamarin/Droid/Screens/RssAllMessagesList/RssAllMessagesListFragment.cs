using System;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Autofac;
using Droid.Screens.Navigation;
using Droid.Screens.RssCreate;
using Shared;
using Shared.Repository;

namespace Droid.Screens.RssAllMessagesList
{
    public class RssAllMessagesListFragment : TitleFragment
    {
        public RssAllMessagesListFragment()
        {
            
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.menu_rssList, menu);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_all_messages_list, container, false);

            Title = Activity.GetText(Resource.String.rssList_title);

            HasOptionsMenu = true;

            var rssMessagesRepository = App.Container.Resolve<IRssMessagesRepository>();

            var items = rssMessagesRepository.GetAllMessages();
            var recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView_allMessages_list);
            recyclerView.SetLayoutManager(new LinearLayoutManager(Context, LinearLayoutManager.Vertical, false));
            var adapter = new RssAllMessagesListAdapter(items, Activity);
            recyclerView.SetAdapter(adapter);

            var fab = view.FindViewById<FloatingActionButton>(Resource.Id.fab_allMessages_addRss);
            fab.Click += OnFabClick;

            return view;
        }

        private void OnFabClick(object sender, EventArgs e)
        {
            RssCreateActivity.StartActivity(Activity);
        }
    }
}