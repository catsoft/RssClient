using Droid.Screens.Navigation;
using Shared.Infrastructure.Navigation;
using Shared.ViewModels.RssCreate;

namespace Droid.Screens.RssCreate
{
    public class RssCreateWay : IWay<RssCreateViewModel>
    {
        private readonly FragmentActivity _fragmentActivity;

        public RssCreateWay(FragmentActivity fragmentActivity)
        {
            _fragmentActivity = fragmentActivity;
        }

        public void Go()
        {
            _fragmentActivity.AddFragment(new RssCreateFragment());
        }
    }
}