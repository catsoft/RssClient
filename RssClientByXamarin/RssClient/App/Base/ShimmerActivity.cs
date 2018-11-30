using Android.OS;
using Com.Facebook.Shimmer;

namespace RssClient.App.Base
{
    public abstract class ShimmerActivity : ToolbarActivity
    {
        public ShimmerFrameLayout ShimmerViewContainer { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ShimmerViewContainer = FindViewById<ShimmerFrameLayout>(Resource.Id.shimmer_view_container);
        }

        protected override void OnResume()
        {
            base.OnResume();
            ShimmerViewContainer?.StartShimmerAnimation();
        }

        protected override void OnPause()
        {
            ShimmerViewContainer.StopShimmerAnimation();
            base.OnPause();
        }
    }
}