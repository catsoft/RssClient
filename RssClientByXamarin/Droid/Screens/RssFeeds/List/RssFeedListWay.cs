using Core.Infrastructure.Navigation;
using Core.ViewModels.RssFeeds.List;
using Droid.Screens.Navigation;

namespace Droid.Screens.RssFeeds.List
{
    public class RssFeedListWay : IWay<RssFeedListViewModel>
    {
        private readonly IFragmentManager _fragmentActivity;

        public RssFeedListWay(IFragmentManager fragmentActivity) { _fragmentActivity = fragmentActivity; }

        public void Go() { _fragmentActivity.AddFragment(new RssFeedListFragment()); }
    }
}
