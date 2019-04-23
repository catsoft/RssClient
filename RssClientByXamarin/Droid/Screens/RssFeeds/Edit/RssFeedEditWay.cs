using Core.Infrastructure.Navigation;
using Core.ViewModels.RssFeeds.Edit;
using Droid.Screens.Navigation;

namespace Droid.Screens.RssFeeds.Edit
{
    public class RssFeedEditWay : IWayWithParameters<RssFeedEditViewModel, RssEditParameters>
    {
        private readonly IFragmentManager _fragmentActivity;
        private readonly RssEditParameters _parameters;

        public RssFeedEditWay(IFragmentManager fragmentActivity, RssEditParameters parameters)
        {
            _fragmentActivity = fragmentActivity;
            _parameters = parameters;
        }

        public void Go()
        {
            var fragment = new RssFeedEditFragment(_parameters.RssId);
            fragment.SetParameters(_parameters);

            _fragmentActivity.AddFragment(fragment);
        }
    }
}
