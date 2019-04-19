#region

using Android.Views;
using Android.Widget;
using JetBrains.Annotations;
using Shared.Extensions;

#endregion

namespace Droid.Screens.RssAllMessagesFilter.Order
{
    public class RssAllMessagesOrderFragmentViewHolder
    {
        public RssAllMessagesOrderFragmentViewHolder([NotNull] View view)
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
