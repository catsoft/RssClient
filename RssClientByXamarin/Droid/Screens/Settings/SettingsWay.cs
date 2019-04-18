using Droid.Screens.Navigation;
using Shared.Infrastructure.Navigation;
using Shared.ViewModels.Settings;

namespace Droid.Screens.Settings
{
    public class SettingsWay : IWay<SettingsViewModel>
    {
        private readonly IFragmentManager _fragmentActivity;

        public SettingsWay(IFragmentManager fragmentActivity)
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