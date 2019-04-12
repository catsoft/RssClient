using Droid.Screens.Navigation;
using Shared.Infrastructure.Navigation;
using Shared.ViewModels.Settings;

namespace Droid.Screens.Settings
{
    public class SettingsWay : IWay<SettingsViewModel>
    {
        private readonly FragmentActivity _fragmentActivity;

        public SettingsWay(FragmentActivity fragmentActivity)
        {
            _fragmentActivity = fragmentActivity;
        }

        public void Go()
        {
            var fragment = new SettingsFragment();

            _fragmentActivity.AddFragment(fragment, CacheState.Replace);
        }
    }
}