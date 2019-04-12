using Droid.Screens.Navigation;
using Shared.Infrastructure.Navigation;
using Shared.ViewModels.RssAllMessages;

namespace Droid.Screens.RssAllMessages
{
    public class RssAllMessagesWay : IWay<RssAllMessagesViewModel>
    {
        private readonly FragmentActivity _activity;

        public RssAllMessagesWay(FragmentActivity activity)
        {
            _activity = activity;
        }

        public void Go()
        {
            _activity.AddFragment(new RssAllMessagesFragment(), CacheState.Old);
        }
    }
}