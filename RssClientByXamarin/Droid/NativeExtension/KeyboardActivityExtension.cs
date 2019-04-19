#region

using Android.App;
using Android.Content;
using Android.Views;
using Android.Views.InputMethods;

#endregion

namespace Droid.NativeExtension
{
    public static class KeyboardActivityExtension
    {
        public static void ShowKeyboard(this Activity activity, View view)
        {
            var manager = (InputMethodManager) activity.GetSystemService(Context.InputMethodService);
            manager?.ShowSoftInput(view, 0);
        }

        public static void HideKeyboard(this Activity activity)
        {
            var focus = activity?.CurrentFocus;
            if (focus != null)
            {
                var manager = (InputMethodManager) activity.GetSystemService(Context.InputMethodService);
                manager?.HideSoftInputFromWindow(focus.WindowToken, 0);
            }
        }
    }
}
