using Android.Widget;

namespace Droid.NativeExtension
{
    public static class EditTextExtension
    {
        public static void SetTextAndSetCursorToLast(this EditText editText, string text)
        {
            editText.SetText("", TextView.BufferType.Editable);
            editText.Append(text);
        }
    }
}