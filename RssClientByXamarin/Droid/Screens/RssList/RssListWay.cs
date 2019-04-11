using Droid.Screens.Navigation;
using Shared.ViewModels.RssList;

namespace Droid.Screens.RssList
{
    public class RssListWay : RssListViewModel.Way
    {
        private readonly FragmentActivity _fragmentActivity;

        public RssListWay(FragmentActivity fragmentActivity)
        {
            _fragmentActivity = fragmentActivity;
        }

        public override void Go()
        {
            _fragmentActivity.AddFragment(new RssListFragment(), CacheState.Old);
        }
    }
}