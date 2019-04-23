using Core.Infrastructure.Navigation;
using Core.ViewModels.Messages.FavoriteMessages;
using Droid.Screens.Navigation;

namespace Droid.Screens.Messages.FavoriteMessagesList
{
    public class FavoriteMessagesListWay : IWay<FavoriteMessagesViewModel>
    {
        private readonly IFragmentManager _activity;

        public FavoriteMessagesListWay(IFragmentManager activity) { _activity = activity; }

        public void Go() { _activity.AddFragment(new FavoriteMessagesListFragment()); }
    }
}
