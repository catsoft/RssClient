using Droid.Screens.About;
using Droid.Screens.Navigation;
using Shared.ViewModels;

namespace Droid.Screens.Settings
{
    public class SettingsWay : RssItemDetailViewModel.Way
    {
        private readonly FragmentActivity _fragmentActivity;

        public SettingsWay(FragmentActivity fragmentActivity)
        {
            _fragmentActivity = fragmentActivity;
        }

        public override void Go()
        {
            var fragment = new SettingsFragment();
            
            _fragmentActivity.AddFragment(fragment, CacheState.Replace);
        }
    }
}