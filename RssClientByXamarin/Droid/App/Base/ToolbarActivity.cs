using Analytics.Rss;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;

namespace RssClient.App.Base
{
    public abstract class ToolbarActivity : AppCompatActivity
    {
        protected abstract int ResourceView { get; }
        protected virtual bool IsDisplayHomeAsUpEnable => true;

        protected Toolbar Toolbar { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(ResourceView);

            Toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(Toolbar);

            var toolbar = SupportActionBar;
            toolbar?.SetDisplayHomeAsUpEnabled(IsDisplayHomeAsUpEnable);

            ScreenLog.Instance.TrackScreenOpen(GetType());
        }

        public override bool OnSupportNavigateUp()
        {
            Finish();
            return true;
        }
    }
}