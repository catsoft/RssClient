using Droid.Screens.Navigation;
using Shared.Infrastructure.Navigation;
using Shared.ViewModels.RssList;

namespace Droid.Screens.RssList
{
    public class RssListWay : IWay<RssListViewModel>
    {
        private readonly FragmentActivity _fragmentActivity;

        public RssListWay(FragmentActivity fragmentActivity)
        {
            _fragmentActivity = fragmentActivity;
        }

        public void Go()
        {
            _fragmentActivity.AddFragment(new RssListFragment(), CacheState.Old);
        }
    }
}