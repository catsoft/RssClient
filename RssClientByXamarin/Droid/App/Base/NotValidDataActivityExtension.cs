using Android.App;
using Android.Views;
using Android.Widget;

namespace RssClient.App.Base
{
    public static class NotValidDataActivityExtension
    {
        private static LinearLayout GetNotValidLayout(Activity activity)
        {
            var notValidLayout = activity.FindViewById<LinearLayout>(Resource.Id.not_valid_layout);
            if (notValidLayout == null)
            {
                var contentLayout = activity.FindViewById<ViewGroup>(Resource.Id.content_layout);
                if (contentLayout == null)
                    return null;

                var view = activity.LayoutInflater.Inflate(Resource.Layout.not_valid_data, contentLayout, true);
                notValidLayout = view.FindViewById<LinearLayout>(Resource.Id.not_valid_layout);
            }

            return notValidLayout;
        }

        private static void SetMainContentVisibility(ViewStates state, Activity activity)
        {
            var mainContentLayout = activity.FindViewById<View>(Resource.Id.main_activity_content);
            if (mainContentLayout != null)
                mainContentLayout.Visibility = state;
        }

        public static void ShowNotValidError(this Activity activity, string error)
        {
            SetMainContentVisibility(ViewStates.Gone, activity);

            var notValidLayout = GetNotValidLayout(activity);

            if (notValidLayout == null) return;

            notValidLayout.Visibility = ViewStates.Visible;
            var textView = notValidLayout.FindViewById<TextView>(Resource.Id.not_valid_textview);
            textView.Text = error;
        }

        public static void ShowValidData(this Activity activity)
        {
            SetMainContentVisibility(ViewStates.Visible, activity);

            var notValidLayout = GetNotValidLayout(activity);

            if (notValidLayout == null) return;
            notValidLayout.Visibility = ViewStates.Gone;
        }
    }
}