using Analytics.Rss;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Autofac;
using Shared;

namespace RssClient.Screens.Base
{
    public abstract class ToolbarActivity : AppCompatActivity
    {
        private ScreenLog _screenLog;

        protected abstract int ResourceView { get; }
        protected virtual bool IsDisplayHomeAsUpEnable => true;

        protected Toolbar Toolbar { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _screenLog = App.Container.Resolve<ScreenLog>();

            SetContentView(ResourceView);

            Toolbar = FindViewById<Toolbar>(Resource.Id.toolbar_toolbarAll_toolbar);
            SetSupportActionBar(Toolbar);

            var toolbar = SupportActionBar;
            toolbar?.SetDisplayHomeAsUpEnabled(IsDisplayHomeAsUpEnable);

            _screenLog.TrackScreenOpen(GetType());
        }

        public override bool OnSupportNavigateUp()
        {
            Finish();
            return true;
        }
    }
}