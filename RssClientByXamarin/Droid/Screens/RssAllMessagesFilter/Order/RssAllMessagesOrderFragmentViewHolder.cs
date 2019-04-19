#region

using Android.Views;
using Android.Widget;
using JetBrains.Annotations;

#endregion

namespace Droid.Screens.RssAllMessagesFilter.Order
{
    public class RssAllMessagesOrderFragmentViewHolder
    {
        public RssAllMessagesOrderFragmentViewHolder([NotNull] View view)
        {
            RootRadioGroup = view.FindViewById<RadioGroup>(Resource.Id.radioGroup_rss_all_messages_order_main);
            NewestRadioButton = view.FindViewById<RadioButton>(Resource.Id.radioButton_rss_all_messages_order_newest);
            OldestRadioButton = view.FindViewById<RadioButton>(Resource.Id.radioButton_rss_all_messages_order_oldest);
        }

        public RadioGroup RootRadioGroup { get; }

        public RadioButton NewestRadioButton { get; }

        public RadioButton OldestRadioButton { get; }
    }
}
