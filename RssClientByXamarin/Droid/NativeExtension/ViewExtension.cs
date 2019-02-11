using Android.Views;

namespace Droid.NativeExtension
{
    public static class ViewExtension
    {
        public static ViewStates ToVisibility(this bool isVisible)
        {
            return isVisible ? ViewStates.Visible : ViewStates.Gone;
        }
    }
}