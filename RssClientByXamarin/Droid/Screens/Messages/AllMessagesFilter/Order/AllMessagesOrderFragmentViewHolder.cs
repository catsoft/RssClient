using Android.Views;
using Android.Widget;
using Droid.NativeExtension;
using JetBrains.Annotations;

namespace Droid.Screens.Messages.AllMessagesFilter.Order
{
    public class AllMessagesOrderFragmentViewHolder
    {
        public AllMessagesOrderFragmentViewHolder([NotNull] View view)
        {
            RootRadioGroup = view.FindNotNull<RadioGroup>(Resource.Id.radioGroup_rss_all_messages_order_main);
            NewestRadioButton = view.FindNotNull<RadioButton>(Resource.Id.radioButton_rss_all_messages_order_newest);
            OldestRadioButton = view.FindNotNull<RadioButton>(Resource.Id.radioButton_rss_all_messages_order_oldest);
        }

        [NotNull] public RadioGroup RootRadioGroup { get; }

        [NotNull] public RadioButton NewestRadioButton { get; }

        [NotNull] public RadioButton OldestRadioButton { get; }
    }
}
