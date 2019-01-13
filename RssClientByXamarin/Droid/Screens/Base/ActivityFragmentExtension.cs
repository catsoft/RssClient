using Android.App;
using Android.Support.V7.App;
using Fragment = Android.Support.V4.App.Fragment;

namespace Droid.Screens.Base
{
    public static class ActivityFragmentExtension
    {
        public static void ReplaceFragmentWithBackstack(this AppCompatActivity activity, Fragment fragment, int containerId)
        {
            activity.SupportFragmentManager.BeginTransaction()
                .Replace(containerId, fragment)
                .AddToBackStack(fragment.GetType().Name)
                .Commit();
        }
    }
}