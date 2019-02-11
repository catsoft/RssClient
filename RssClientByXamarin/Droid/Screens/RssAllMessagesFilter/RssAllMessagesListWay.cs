using Droid.Screens.Navigation;
using Shared.ViewModels;

namespace Droid.Screens.RssAllMessagesFilter
{
    public class RssAllMessagesFilterWay : RssAllMessagesFilterViewModel.Way
    {
        private readonly FragmentActivity _activity;

        public RssAllMessagesFilterWay(FragmentActivity activity)
        {
            _activity = activity;
        }

        public override void Go()
        {
            _activity.AddFragment(new RssAllMessagesFilterFragment(), CacheState.Old);
        }
    }
}