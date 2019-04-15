using Droid.Screens.Navigation;
using Shared.Infrastructure.Navigation;
using Shared.ViewModels.RssItemDetail;

namespace Droid.Screens.RssItemMessage
{
    public class RssItemDetailWay : IWayWithParameters<RssItemDetailViewModel, RssItemDetailParameters>
    {
        private readonly FragmentActivity _fragmentActivity;
        private readonly RssItemDetailParameters _parameters;

        public RssItemDetailWay(FragmentActivity fragmentActivity, RssItemDetailParameters parameters)
        {
            _fragmentActivity = fragmentActivity;
            _parameters = parameters;
        }

        public void Go()
        {
            var fragment = new RssItemDetailFragment(_parameters.RssModel.Id);
            fragment.SetParameters(_parameters);
            
            _fragmentActivity.AddFragment(fragment, CacheState.Replace);
        }
    }
}