using Android.OS;
using Android.Support.V4.App;
using Android.Views;

namespace Droid.Screens.Rss.List
{
    public class RssAllMessagesListFragment : Fragment
    {
        public RssAllMessagesListFragment()
        {
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_all_messages_list, container, false);

            return view;
        }
    }
}