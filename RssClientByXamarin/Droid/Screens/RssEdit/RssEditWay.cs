using Droid.Screens.Navigation;
using Shared.Services.Navigator;
using Shared.ViewModels;

namespace Droid.Screens.RssEdit
{
    public class RssEditWay : DataWay<RssEditViewModel.Way.WayData>
    {
        private readonly FragmentActivity _fragmentActivity;

        public RssEditWay(FragmentActivity fragmentActivity)
        {
            _fragmentActivity = fragmentActivity;
        }

        public override void Go()
        {
            _fragmentActivity.AddFragment(new RssEditFragment(Data.RssId));
        }
    }
}