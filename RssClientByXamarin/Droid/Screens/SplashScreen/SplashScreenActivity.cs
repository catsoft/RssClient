#region

using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Droid.Screens.Main;

#endregion

namespace Droid.Screens.SplashScreen
{
    [Activity(Label = "@string/all_appName", Theme = "@style/AppTheme.Default.Launcher", MainLauncher = true, NoHistory = true)]
    public class SplashScreenActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            MainActivity.CreateActivity(this);
        }
    }
}
