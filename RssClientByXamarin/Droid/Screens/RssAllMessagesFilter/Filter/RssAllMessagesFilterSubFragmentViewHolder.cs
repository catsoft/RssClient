using Android.Views;
using Android.Widget;
using JetBrains.Annotations;
using Shared.Extensions;

namespace Droid.Screens.RssAllMessagesFilter.Filter
{
    public class RssAllMessagesFilterSubFragmentViewHolder
    {
        public RssAllMessagesFilterSubFragmentViewHolder([NotNull] View view)
        {
            RootRadioGroup = view.FindViewById<RadioGroup>(Resource.Id.radioGroup_rss_all_messages_filter_main).NotNull();
            AllRadioButton = view.FindViewById<RadioButton>(Resource.Id.radioButton_rss_all_messages_filter_all).NotNull();
            FavoriteRadioButton = view.FindViewById<RadioButton>(Resource.Id.radioButton_rss_all_messages_filter_favorite).NotNull();
            ReadRadioButton = view.FindViewById<RadioButton>(Resource.Id.radioButton_rss_all_messages_filter_read).NotNull();
            UnreadRadioButton = view.FindViewById<RadioButton>(Resource.Id.radioButton_rss_all_messages_filter_unread).NotNull();

            FromButton = view.FindViewById<Button>(Resource.Id.button_AllMessagesFilter_dateFrom).NotNull();
            ToButton = view.FindViewById<Button>(Resource.Id.button_AllMessagesFilter_dateTo).NotNull();
        }

        [NotNull] public RadioGroup RootRadioGroup { get; }

        [NotNull] public RadioButton AllRadioButton { get; }

        [NotNull] public RadioButton FavoriteRadioButton { get; }

        [NotNull] public RadioButton ReadRadioButton { get; }

        [NotNull] public RadioButton UnreadRadioButton { get; }

        [NotNull] public Button FromButton { get; }

        [NotNull] public Button ToButton { get; }
    }
}
