using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Droid.Screens.Main;

namespace Droid.Screens.SplashScreen
{
    [Activity(Label = "@string/all_appName", Theme = "@style/AppTheme.Launcher", MainLauncher = true)]
    public class SplashScreenActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            MainActivity.CreateActivity(this);
        }
    }
}