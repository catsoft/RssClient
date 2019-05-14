using Android.Views;
using Android.Widget;
using Droid.NativeExtension;
using JetBrains.Annotations;

namespace Droid.Screens.Donate
{
    public class DonateFragmentViewHolder
    {
        public DonateFragmentViewHolder([NotNull] View view)
        {
            PayContainerLinearLayout = view.FindNotNull<LinearLayout>(Resource.Id.linearLayout_donate_payContainer);
            PriceTextView = view.FindNotNull<TextView>(Resource.Id.textView_donate_textView);
        }
        
        [NotNull] public LinearLayout PayContainerLinearLayout { get; }
        
        [NotNull] public TextView PriceTextView { get; }
    }
}