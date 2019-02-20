using Droid.Screens.FeedlySearch;
using Droid.Screens.Navigation;
using Shared.ViewModels;

namespace Droid.Screens.RecommendedCategoryList
{
    public class RecommendedCategoryListWay : RecommendedCategoryListViewModel.Way
    {
        private readonly FragmentActivity _fragmentActivity;

        public RecommendedCategoryListWay(FragmentActivity fragmentActivity)
        {
            _fragmentActivity = fragmentActivity;
        }

        public override void Go()
        {
            _fragmentActivity.AddFragment(new FeedlySearchFragment(), CacheState.Replace);
        }
    }
}