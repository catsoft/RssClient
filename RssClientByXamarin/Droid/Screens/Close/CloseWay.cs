using Android.App;
using Core.Infrastructure.Navigation;
using Core.ViewModels.Close;

namespace Droid.Screens.Close
{
    public class CloseWay : IWay<CloseViewModel>
    {
        private readonly Activity _activity;

        public CloseWay(Activity activity) { _activity = activity; }

        public void Go() { _activity.OnBackPressed(); }
    }
}
