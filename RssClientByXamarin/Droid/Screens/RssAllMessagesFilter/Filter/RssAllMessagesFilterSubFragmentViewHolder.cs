#region

using Android.Views;
using Android.Widget;

#endregion

namespace Droid.Screens.RssAllMessagesFilter.Filter
{
    public class RssAllMessagesFilterSubFragmentViewHolder
    {
        public RssAllMessagesFilterSubFragmentViewHolder(View view)
        {
            RootRadioGroup = view.FindViewById<RadioGroup>(Resource.Id.radioGroup_rss_all_messages_filter_main);
            AllRadioButton = view.FindViewById<RadioButton>(Resource.Id.radioButton_rss_all_messages_filter_all);
            FavoriteRadioButton = view.FindViewById<RadioButton>(Resource.Id.radioButton_rss_all_messages_filter_favorite);
            ReadRadioButton = view.FindViewById<RadioButton>(Resource.Id.radioButton_rss_all_messages_filter_read);
            UnreadRadioButton = view.FindViewById<RadioButton>(Resource.Id.radioButton_rss_all_messages_filter_unread);

            FromButton = view.FindViewById<Button>(Resource.Id.button_AllMessagesFilter_dateFrom);
            ToButton = view.FindViewById<Button>(Resource.Id.button_AllMessagesFilter_dateTo);
        }

        public RadioGroup RootRadioGroup { get; }

        public RadioButton AllRadioButton { get; }

        public RadioButton FavoriteRadioButton { get; }

        public RadioButton ReadRadioButton { get; }

        public RadioButton UnreadRadioButton { get; }

        public Button FromButton { get; }

        public Button ToButton { get; }
    }
}
