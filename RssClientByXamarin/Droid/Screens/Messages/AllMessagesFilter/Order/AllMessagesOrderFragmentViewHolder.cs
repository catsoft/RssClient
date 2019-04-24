using Android.Views;
using Android.Widget;
using Core.Extensions;
using Droid.Resources;
using JetBrains.Annotations;

namespace Droid.Screens.Messages.AllMessagesFilter.Order
{
    public class AllMessagesOrderFragmentViewHolder
    {
        public AllMessagesOrderFragmentViewHolder([NotNull] View view)
        {
            RootRadioGroup = view.FindViewById<RadioGroup>(Resource.Id.radioGroup_rss_all_messages_order_main).NotNull();
            NewestRadioButton = view.FindViewById<RadioButton>(Resource.Id.radioButton_rss_all_messages_order_newest).NotNull();
            OldestRadioButton = view.FindViewById<RadioButton>(Resource.Id.radioButton_rss_all_messages_order_oldest).NotNull();
        }

        [NotNull] public RadioGroup RootRadioGroup { get; }

        [NotNull] public RadioButton NewestRadioButton { get; }

        [NotNull] public RadioButton OldestRadioButton { get; }
    }
}
