using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Autofac;
using Shared;
using Shared.Analytics.Rss;

namespace Droid.Screens.Base
{
    public abstract class ToolbarActivity : AppCompatActivity
    {
        protected abstract int ResourceView { get; }
        protected Toolbar Toolbar { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(ResourceView);

            Toolbar = FindViewById<Toolbar>(Resource.Id.toolbar_toolbarAll_toolbar);
            SetSupportActionBar(Toolbar);

            App.Container.Resolve<ScreenLog>().TrackScreenOpen(GetType());
        }

        public override bool OnSupportNavigateUp()
        {
            Finish();
            return true;
        }
    }
}