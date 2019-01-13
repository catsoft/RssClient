using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Views;
using Autofac;
using Shared;
using Shared.Repository;

namespace Droid.Screens.Rss.List.RssAllMessagesList
{
    public class RssAllMessagesListFragment : Fragment
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

            HasOptionsMenu = true;

            var rssMessagesRepository = App.Container.Resolve<IRssMessagesRepository>();

            var items = rssMessagesRepository.GetAllMessages();
            var recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView_allMessages_list);
            recyclerView.SetLayoutManager(new LinearLayoutManager(Context, LinearLayoutManager.Vertical, false));
            var adapter = new RssAllMessagesListAdapter(items, Activity);
            recyclerView.SetAdapter(adapter);

            return view;
        }
    }
}