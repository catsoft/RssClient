using Droid.Screens.Navigation;
using Shared.ViewModels;

namespace Droid.Screens.RssItemDetail
{
    public class Way : RssItemDetailViewModel.Way
    {
        private readonly FragmentActivity _fragmentActivity;

        public Way(FragmentActivity fragmentActivity)
        {
            _fragmentActivity = fragmentActivity;
        }

        public override void Go()
        {
            var fragment = new RssDetailItemFragment(Data.RssModel.Id);

            _fragmentActivity.AddFragment(fragment);
        }
    }
}