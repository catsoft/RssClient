using Droid.Screens.Navigation;
using Shared.Infrastructure.Navigation;
using Shared.ViewModels.RssFavoriteMessages;

namespace Droid.Screens.RssFavoriteMessagesList
{
    public class RssFavoriteMessagesListWay : IWay<RssFavoriteMessagesViewModel>
    {
        private readonly IFragmentManager _activity;

        public RssFavoriteMessagesListWay(IFragmentManager activity)
        {
            _activity = activity;
        }

        public void Go()
        {
            _activity.AddFragment(new RssFavoriteMessagesListFragment(), CacheState.Old);
        }
    }
}