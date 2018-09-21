using Android.Widget;

namespace RssClient.App.Base
{
    public static class CursorLastPositionExtension
    {
        public static void SetTextAndSetCursorToLast(this EditText editText, string text)
        {
            editText.SetText("", TextView.BufferType.Editable);
            editText.Append(text);
        }
    }
}