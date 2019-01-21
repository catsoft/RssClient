using Droid.Screens.Navigation;
using Shared.ViewModels;

namespace Droid.Screens.RecommendedRssList
{
    public class RecommendedRssListWay : RecommendationViewModel.Way
    {
        private readonly FragmentActivity _fragmentActivity;

        public RecommendedRssListWay(FragmentActivity fragmentActivity)
        {
            _fragmentActivity = fragmentActivity;
        }
        
        public override void Go()
        {
            _fragmentActivity.AddFragment(new RecommendedRssListFragment(), CacheState.Replace);
        }
    }
}