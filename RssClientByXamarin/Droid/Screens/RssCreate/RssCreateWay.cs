using Droid.Screens.Navigation;
using Shared.ViewModels;

namespace Droid.Screens.RssCreate
{
    public class RssCreateWay : RssCreateViewModel.Way
    {
        private readonly FragmentActivity _fragmentActivity;

        public RssCreateWay(FragmentActivity fragmentActivity)
        {
            _fragmentActivity = fragmentActivity;
        }

        public override void Go()
        {
            _fragmentActivity.AddFragment(new RssCreateFragment());
        }
    }
}