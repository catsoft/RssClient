using Core.Infrastructure.Navigation;
using Core.ViewModels.RssFeeds.Create;
using Droid.Screens.Navigation;

namespace Droid.Screens.RssFeeds.Create
{
    public class RssFeedCreateWay : IWay<RssFeedCreateViewModel>
    {
        private readonly IFragmentManager _fragmentActivity;

        public RssFeedCreateWay(IFragmentManager fragmentActivity) { _fragmentActivity = fragmentActivity; }

        public void Go() { _fragmentActivity.AddFragment(new RssFeedCreateFragment()); }
    }
}
