using System.Collections.Generic;
using Android.App;
using Android.Support.Design.Widget;
using Android.Widget;

namespace RssClient.Screens.Base
{
	public static class ShowErrorExtension
    {
        public static void ShowError(this Activity activity, string message)
        {
            activity.RunOnUiThread(() =>
            {
                Toast.MakeText(activity, message, ToastLength.Long).Show();
            });
        }

        public static void ShowFieldError<T>(this Activity activity, Dictionary<T, TextInputLayout> fields, T field, string error)
        {
            if (fields.TryGetValue(field, out var textInput))
            {
                activity.RunOnUiThread(() =>
                {
                    textInput.ErrorEnabled = true;
                    textInput.Error = error;
                });
            }
        }

        public static void ShowNotError(this Activity activity, IEnumerable<TextInputLayout> textInputs)
        {
            activity.RunOnUiThread(() =>
            {
                foreach (var textInput in textInputs)
                {
                    textInput.ErrorEnabled = false;
                    textInput.Error = string.Empty;
                }
            });
        }
    }
}