using Core.Infrastructure.Navigation;
using Core.ViewModels.FeedlySearch;
using Droid.Screens.Navigation;

namespace Droid.Screens.FeedlySearch
{
    public class FeedlySearchWay : IWay<FeedlySearchViewModel>
    {
        private readonly IFragmentManager _activity;

        public FeedlySearchWay(IFragmentManager activity) { _activity = activity; }

        public void Go() { _activity.AddFragment(new FeedlySearchFragment()); }
    }
}
