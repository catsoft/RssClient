using Droid.Screens.Navigation;
using Shared.Infrastructure.Navigation;
using Shared.ViewModels.RssListEdit;

namespace Droid.Screens.RssEditList
{
    public class RssListEditWay : IWay<RssListEditViewModel>
    {
        private readonly IFragmentManager _activity;

        public RssListEditWay(IFragmentManager activity) { _activity = activity; }

        public void Go() { _activity.AddFragment(new RssEditListFragment()); }
    }
}
