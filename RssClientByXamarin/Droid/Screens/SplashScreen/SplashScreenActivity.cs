﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Droid.Screens.Rss.List;

namespace Droid.Screens.SplashScreen
{
    [Activity(Label = "@string/all_appName", Theme = "@style/AppTheme.Launcher", MainLauncher = true)]
    public class SplashScreenActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var intent = new Intent(this, typeof(RssListActivity));
            StartActivity(intent);
            Finish();
        }
    }
}