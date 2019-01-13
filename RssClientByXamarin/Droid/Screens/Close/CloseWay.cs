using Android.App;
using Shared.Database;
using Shared.ViewModels;

namespace Droid.Screens.Close
{
    public class CloseWay : CloseViewModel.CloseWay
    {
        private readonly Activity _activity;

        public CloseWay(RealmDatabase database, Activity activity) : base(database)
        {
            _activity = activity;
        }

        public override void Go()
        {
            _activity.OnBackPressed();
        }
    }
}