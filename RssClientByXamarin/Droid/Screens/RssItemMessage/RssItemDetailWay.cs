using Droid.Screens.Navigation;
using Shared.Infrastructure.Navigation;
using Shared.ViewModels.RssItemDetail;

namespace Droid.Screens.RssItemMessage
{
    public class RssItemDetailWay : IWayWithParameters<RssItemDetailViewModel, RssItemDetailParameterses>
    {
        private readonly FragmentActivity _fragmentActivity;
        private readonly RssItemDetailParameterses _parameters;

        public RssItemDetailWay(FragmentActivity fragmentActivity, RssItemDetailParameterses parameters)
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