using Droid.Screens.Navigation;
using Droid.Screens.RssAllMessagesFilter;
using Shared.ViewModels;

namespace Droid.Screens.FeedlySearch
{
    public class FeedlySearchWay : FeedlySearchViewModel.Way
    {
        private readonly FragmentActivity _activity;

        public FeedlySearchWay(FragmentActivity activity)
        {
            _activity = activity;
        }

        public override void Go()
        {
            _activity.AddFragment(new FeedlySearchFragment(), CacheState.Old);
        }
    }
}