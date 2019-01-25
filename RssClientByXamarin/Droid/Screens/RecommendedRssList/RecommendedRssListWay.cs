using Droid.Screens.Navigation;
using Shared.ViewModels;

namespace Droid.Screens.RecommendedRssList
{
    public class RecommendedRssListWay : RecommendedViewModel.Way
    {
        private readonly FragmentActivity _fragmentActivity;

        public RecommendedRssListWay(FragmentActivity fragmentActivity)
        {
            _fragmentActivity = fragmentActivity;
        }
        
        public override void Go()
        {
            _fragmentActivity.AddFragment(new RecommendedRssListFragment(Data.Categories), CacheState.Replace);
        }
    }
}