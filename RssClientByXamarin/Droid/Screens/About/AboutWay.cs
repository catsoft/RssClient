using Droid.Screens.Navigation;
using Shared.ViewModels.About;

namespace Droid.Screens.About
{
    public class AboutWay : AboutViewModel.Way
    {
        private readonly FragmentActivity _fragmentActivity;

        public AboutWay(FragmentActivity fragmentActivity)
        {
            _fragmentActivity = fragmentActivity;
        }

        public override void Go()
        {
            var fragment = new AboutFragment();

            _fragmentActivity.AddFragment(fragment, CacheState.Replace);
        }
    }
}