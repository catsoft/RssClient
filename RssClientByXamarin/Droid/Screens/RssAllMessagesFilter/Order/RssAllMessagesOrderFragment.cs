
using Android.OS;
using Android.Views;
using Droid.Screens.Navigation;

namespace Droid.Screens.RssAllMessagesFilter.Order
{
    public class RssAllMessagesOrderFragment : SubFragment
    {
        protected override int LayoutId => Resource.Layout.fragment_all_messages_order_sub;

        public RssAllMessagesOrderFragment()
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