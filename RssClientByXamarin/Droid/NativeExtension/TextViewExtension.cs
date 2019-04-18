using Android.OS;
using Android.Text;
using Android.Widget;

namespace Droid.NativeExtension
{
    public static class TextViewExtension
    {
        public static void SetTextAsHtml(this TextView textView, string text)
        {
            var spanned = Build.VERSION.SdkInt >= BuildVersionCodes.N
                ? Html.FromHtml(text, FromHtmlOptions.ModeLegacy)
                : Html.FromHtml(text);
            textView.SetText(spanned, TextView.BufferType.Spannable);
        }
    }
}