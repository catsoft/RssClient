using Droid.Screens.Navigation;
using Shared.Infrastructure.Navigation;
using Shared.ViewModels.RssItemDetail;

namespace Droid.Screens.RssMessagesList
{
    public class RssMessagesListWay : IWayWithParameters<RssMessagesListViewModel, RssMessagesListParameters>
    {
        private readonly IFragmentManager _fragmentActivity;
        private readonly RssMessagesListParameters _parameters;

        public RssMessagesListWay(IFragmentManager fragmentActivity, RssMessagesListParameters parameters)
        {
            _fragmentActivity = fragmentActivity;
            _parameters = parameters;
        }

        public void Go()
        {
            var fragment = new RssMessagesListFragment(_parameters.RssModel.Id);
            fragment.SetParameters(_parameters);
            
            _fragmentActivity.AddFragment(fragment, CacheState.Replace);
        }
    }
}