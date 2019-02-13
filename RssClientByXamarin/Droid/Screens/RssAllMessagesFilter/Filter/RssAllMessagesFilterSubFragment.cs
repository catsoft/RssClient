using Android.OS;
using Android.Views;
using Droid.Screens.Navigation;

namespace Droid.Screens.RssAllMessagesFilter.Filter
{
    public class RssAllMessagesFilterSubFragment : SubFragment
    {
        protected override int LayoutId => Resource.Layout.fragment_all_messages_filter_sub;

        public RssAllMessagesFilterSubFragment()
        {
            
        }

        protected override void RestoreState(Bundle saved)
        {
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            return view;
        }
    }
}