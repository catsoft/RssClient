using Core.Infrastructure.Navigation;
using Core.ViewModels.Settings;
using Droid.Screens.Navigation;

namespace Droid.Screens.Settings
{
    public class SettingsWay : IWay<SettingsViewModel>
    {
        private readonly IFragmentManager _fragmentActivity;

        public SettingsWay(IFragmentManager fragmentActivity) { _fragmentActivity = fragmentActivity; }

        public void Go()
        {
            var fragment = new SettingsFragment();

            _fragmentActivity.AddFragment(fragment);
        }
    }
}
