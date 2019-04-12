using Droid.Screens.Navigation;
using Shared.Infrastructure.Navigation;
using Shared.ViewModels.RssFavoriteMessages;

namespace Droid.Screens.RssFavoriteMessagesList
{
    public class RssFavoriteMessagesListWay : IWay<RssFavoriteMessagesViewModel>
    {
        private readonly FragmentActivity _activity;

        public RssFavoriteMessagesListWay(FragmentActivity activity)
        {
            _activity = activity;
        }

        public void Go()
        {
            _activity.AddFragment(new RssFavoriteMessagesListFragment(), CacheState.Old);
        }
    }
}