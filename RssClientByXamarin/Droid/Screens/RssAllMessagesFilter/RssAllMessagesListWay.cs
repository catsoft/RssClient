using Droid.Screens.Navigation;
using Shared.Infrastructure.Navigation;
using Shared.ViewModels.RssAllMessagesFilter;

namespace Droid.Screens.RssAllMessagesFilter
{
    public class RssAllMessagesFilterWay : IWay<RssAllMessagesFilterViewModel>
    {
        private readonly IFragmentManager _activity;

        public RssAllMessagesFilterWay(IFragmentManager activity) { _activity = activity; }

        public void Go() { _activity.AddFragment(new RssAllMessagesFilterFragment(), CacheState.Old); }
    }
}
