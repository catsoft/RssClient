using System;
using System.Collections.Generic;
using Android.App;
using Android.Support.Design.Widget;
using Android.Widget;
using Shared.App.Base;
using Shared.App.Base.Command;

namespace RssClient.App.Base
{
    public static class ShowErrorExtension
    {
        public static void ShowError(this Activity activity, Error error)
        {
            activity.RunOnUiThread(() =>
            {
                Toast.MakeText(activity, error.Message, ToastLength.Long).Show();
            });
        }

        public static void ShowErrorNotInternet(this Activity activity)
        {
            activity.RunOnUiThread(() =>
            {
                Toast.MakeText(activity, AppString.ErrorNotInternet, ToastLength.Long).Show();
            });
        }

        public static void ShowFieldError<T>(this Activity activity, Dictionary<T, TextInputLayout> fields, T field, Error error)
        {
            if (fields.TryGetValue(field, out var textInput))
            {
                activity.RunOnUiThread(() =>
                {
                    textInput.ErrorEnabled = true;
                    textInput.Error = error.Message;
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

        public static CommandDelegate<T> GetCommandDelegate<T>(this Activity activity, Action<T> onSuccess)
            where T : BaseResponse
        {
            var @delegate = new CommandDelegate<T>(onSuccess, (error) => ShowError(activity, error), () => ShowErrorNotInternet(activity));
            return @delegate;
        }
    }
}