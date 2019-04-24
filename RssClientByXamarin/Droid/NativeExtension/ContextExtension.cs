using Android.Content;
using Android.Widget;
using Droid.Resources;

namespace Droid.NativeExtension
{
    public static class ContextExtension
    {
        public static void Toast(this Context context, string text, ToastLength length = ToastLength.Short)
        {
            Android.Widget.Toast.MakeText(context, text, length).Show();
        }

        public static void ToastClipboard(this Context context, string text, ToastLength length = ToastLength.Short)
        {
            var textClip = context.GetText(Resource.String.copy_clipboard);
            Android.Widget.Toast.MakeText(context, $"{textClip} {text}", length).Show();
        }
    }
}
