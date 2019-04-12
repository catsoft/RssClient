using Droid.Screens.Navigation;
using Shared.Infrastructure.Navigation;
using Shared.ViewModels.RssListEdit;

namespace Droid.Screens.RssEditList
{
    public class RssListEditWay : IWay<RssListEditViewModel>
    {
        private readonly FragmentActivity _activity;

        public RssListEditWay(FragmentActivity activity)
        {
            _activity = activity;
        }
        
        public void Go()
        {
            _activity.AddFragment(new RssEditListFragment(), CacheState.Replace);
        }
    }
}