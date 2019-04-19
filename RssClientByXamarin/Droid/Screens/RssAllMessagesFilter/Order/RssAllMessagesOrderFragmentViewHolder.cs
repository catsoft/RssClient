#region

using Android.Views;
using Android.Widget;

#endregion

namespace Droid.Screens.RssAllMessagesFilter.Order
{
    public class RssAllMessagesOrderFragmentViewHolder
    {
        public RssAllMessagesOrderFragmentViewHolder(View view)
        {
            RootRadioGroup =  view.FindViewById<RadioGroup>(Resource.Id.radioGroup_rss_all_messages_order_main);
            NewestRadioButton = view.FindViewById<RadioButton>(Resource.Id.radioButton_rss_all_messages_order_newest);
            OldestRadioButton = view.FindViewById<RadioButton>(Resource.Id.radioButton_rss_all_messages_order_oldest);
        }

        public RadioGroup RootRadioGroup { get; set; }

        public RadioButton NewestRadioButton { get; set; }

        public RadioButton OldestRadioButton { get; set; }
    }
}
