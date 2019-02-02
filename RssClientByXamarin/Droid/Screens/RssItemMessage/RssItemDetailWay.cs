using Droid.Screens.Navigation;
using Shared.ViewModels;

namespace Droid.Screens.RssItemMessage
{
    public class RssItemDetailWay : RssItemDetailViewModel.Way
    {
        private readonly FragmentActivity _fragmentActivity;

        public RssItemDetailWay(FragmentActivity fragmentActivity)
        {
            _fragmentActivity = fragmentActivity;
        }

        public override void Go()
        {
            var fragment = new RssItemDetailFragment(Data.RssModel.Id);

            _fragmentActivity.AddFragment(fragment, CacheState.Replace);
        }
    }
}