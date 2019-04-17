using Droid.Screens.Navigation;
using Shared.Infrastructure.Navigation;
using Shared.ViewModels.RssEdit;

namespace Droid.Screens.RssEdit
{
    public class RssEditWay : IWayWithParameters<RssEditViewModel, RssEditParameters>
    {
        private readonly IFragmentManager _fragmentActivity;
        private readonly RssEditParameters _parameters;

        public RssEditWay(IFragmentManager fragmentActivity, RssEditParameters parameters)
        {
            _fragmentActivity = fragmentActivity;
            _parameters = parameters;
        }

        public void Go()
        {
            var fragment = new RssEditFragment(_parameters.RssId);
            fragment.SetParameters(_parameters);
            
            _fragmentActivity.AddFragment(fragment);
        }
    }
}