using Droid.Screens.Navigation;
using Shared.ViewModels.RssFavoriteMessages;

namespace Droid.Screens.RssFavoriteMessagesList
{
    public class RssFavoriteMessagesListWay : RssFavoriteMessagesViewModel.Way
    {
        private readonly FragmentActivity _activity;

        public RssFavoriteMessagesListWay(FragmentActivity activity)
        {
            _activity = activity;
        }

        public override void Go()
        {
            _activity.AddFragment(new RssFavoriteMessagesListFragment(), CacheState.Old);
        }
    }
}