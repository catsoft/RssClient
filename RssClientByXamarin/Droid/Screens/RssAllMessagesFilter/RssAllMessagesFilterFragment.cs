using Android.OS;
using Android.Views;
using Droid.Screens.Navigation;

namespace Droid.Screens.RssAllMessagesFilter
{
    public class RssAllMessagesFilterFragment : TitleFragment
    {
        protected override int LayoutId => Resource.Layout.fragment_all_messages_filter;
        public override bool RootFragment => false;
        
        protected override void RestoreState(Bundle saved)
        {
        }

        public RssAllMessagesFilterFragment()
        {
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            
            return view;
        }
    }
}