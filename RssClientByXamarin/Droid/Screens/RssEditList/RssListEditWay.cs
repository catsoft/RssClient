using Droid.Screens.Navigation;
using Shared.ViewModels;

namespace Droid.Screens.RssEditList
{
    public class RssListEditWay : RssListEditViewModel.Way
    {
        private readonly FragmentActivity _activity;

        public RssListEditWay(FragmentActivity activity)
        {
            _activity = activity;
        }
        
        public override void Go()
        {
            _activity.AddFragment(new RssEditListFragment(), CacheState.Replace);
        }
    }
}