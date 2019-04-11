using Droid.Screens.Navigation;
using Shared.ViewModels.RssEdit;

namespace Droid.Screens.RssEdit
{
    public class RssEditWay : RssEditViewModel.Way
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