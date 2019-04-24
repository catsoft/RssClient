using Core.Infrastructure.Navigation;
using Core.ViewModels.Messages.RssFeedMessagesList;
using Droid.Screens.Navigation;

namespace Droid.Screens.Messages.RssFeedMessagesList
{
    public class RssFeedMessagesListWay : IWayWithParameters<RssFeedMessagesListViewModel, RssFeedMessagesListParameters>
    {
        private readonly IFragmentManager _fragmentActivity;
        private readonly RssFeedMessagesListParameters _parameters;

        public RssFeedMessagesListWay(IFragmentManager fragmentActivity, RssFeedMessagesListParameters parameters)
        {
            _fragmentActivity = fragmentActivity;
            _parameters = parameters;
        }

        public void Go()
        {
            var fragment = new RssFeedMessagesListFragment(_parameters.RssFeedModel.Id);
            fragment.SetParameters(_parameters);

            _fragmentActivity.AddFragment(fragment);
        }
    }
}
