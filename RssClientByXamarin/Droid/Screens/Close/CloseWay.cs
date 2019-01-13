using Android.App;
using Shared.ViewModels;

namespace Droid.Screens.Close
{
    public class CloseWay : CloseViewModel.Way
    {
        private readonly Activity _activity;

        public CloseWay(Activity activity)
        {
            _activity = activity;
        }

        public override void Go()
        {
            _activity.OnBackPressed();
        }
    }
}