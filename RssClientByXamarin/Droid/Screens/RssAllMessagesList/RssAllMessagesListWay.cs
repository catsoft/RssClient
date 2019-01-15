using Droid.Screens.Navigation;
using Shared.ViewModels;

namespace Droid.Screens.RssAllMessagesList
{
    public class RssAllMessagesListWay : RssAllMessagesViewModel.Way
    {
        private readonly FragmentActivity _activity;

        public RssAllMessagesListWay(FragmentActivity activity)
        {
            _activity = activity;
        }
        
        public override void Go()
        {
            _activity.AddFragment(new RssAllMessagesListFragment(), CacheState.Old);
        }
    }
}