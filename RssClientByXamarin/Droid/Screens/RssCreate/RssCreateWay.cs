using Droid.Screens.Navigation;
using Shared.Infrastructure.Navigation;
using Shared.ViewModels.RssCreate;

namespace Droid.Screens.RssCreate
{
    public class RssCreateWay : IWay<RssCreateViewModel>
    {
        private readonly IFragmentManager _fragmentActivity;

        public RssCreateWay(IFragmentManager fragmentActivity) { _fragmentActivity = fragmentActivity; }

        public void Go() { _fragmentActivity.AddFragment(new RssCreateFragment()); }
    }
}
