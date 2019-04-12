using Droid.Screens.Navigation;
using Shared.Infrastructure.Navigation;
using Shared.ViewModels.RssEdit;
using Shared.ViewModels.Settings;

namespace Droid.Screens.RssEdit
{
    public class RssEditWay : IWayWithParameters<RssEditViewModel, RssEditParameterses>
    {
        private readonly FragmentActivity _fragmentActivity;
        private readonly RssEditParameterses _parameters;

        public RssEditWay(FragmentActivity fragmentActivity, RssEditParameterses parameters)
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