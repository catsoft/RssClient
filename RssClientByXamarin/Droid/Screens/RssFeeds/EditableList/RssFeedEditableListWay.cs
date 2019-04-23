using Core.Infrastructure.Navigation;
using Core.ViewModels.RssFeeds.EditableList;
using Droid.Screens.Navigation;

namespace Droid.Screens.RssFeeds.EditableList
{
    public class RssFeedEditableListWay : IWay<RssFeedEditableListViewModel>
    {
        private readonly IFragmentManager _activity;

        public RssFeedEditableListWay(IFragmentManager activity) { _activity = activity; }

        public void Go() { _activity.AddFragment(new RssFeedEditableListFragment()); }
    }
}
