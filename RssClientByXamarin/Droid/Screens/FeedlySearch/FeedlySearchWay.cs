using Droid.Screens.Navigation;
using Shared.Infrastructure.Navigation;
using Shared.ViewModels.FeedlySearch;

namespace Droid.Screens.FeedlySearch
{
    public class FeedlySearchWay : IWay<FeedlySearchViewModel>
    {
        private readonly IFragmentManager _activity;

        public FeedlySearchWay(IFragmentManager activity) { _activity = activity; }

        public void Go() { _activity.AddFragment(new FeedlySearchFragment()); }
    }
}
