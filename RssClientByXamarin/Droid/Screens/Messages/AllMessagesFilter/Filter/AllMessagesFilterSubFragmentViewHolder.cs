using Android.Views;
using Android.Widget;
using Droid.NativeExtension;
using JetBrains.Annotations;

namespace Droid.Screens.Messages.AllMessagesFilter.Filter
{
    public class AllMessagesFilterSubFragmentViewHolder
    {
        public AllMessagesFilterSubFragmentViewHolder([NotNull] View view)
        {
            RootRadioGroup = view.FindNotNull<RadioGroup>(Resource.Id.radioGroup_rss_all_messages_filter_main);
            AllRadioButton = view.FindNotNull<RadioButton>(Resource.Id.radioButton_rss_all_messages_filter_all);
            FavoriteRadioButton = view.FindNotNull<RadioButton>(Resource.Id.radioButton_rss_all_messages_filter_favorite);
            ReadRadioButton = view.FindNotNull<RadioButton>(Resource.Id.radioButton_rss_all_messages_filter_read);
            UnreadRadioButton = view.FindNotNull<RadioButton>(Resource.Id.radioButton_rss_all_messages_filter_unread);

            FromButton = view.FindNotNull<Button>(Resource.Id.button_AllMessagesFilter_dateFrom);
            ToButton = view.FindNotNull<Button>(Resource.Id.button_AllMessagesFilter_dateTo);
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
