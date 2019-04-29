using Android.App;
using Android.Widget;
using Core.Infrastructure.Dialogs;

namespace Droid.Infrastructure.Dialogs
{
    public class ToastService : IToastService
    {
        private readonly Activity _activity;

        public ToastService(Activity activity)
        {
            _activity = activity;
        }
        
        public void Show(string text)
        {
            Toast.MakeText(_activity, text, ToastLength.Short).Show();
        }
    }
}