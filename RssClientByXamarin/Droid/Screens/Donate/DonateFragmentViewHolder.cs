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
        }
        
        [NotNull] public LinearLayout PayContainerLinearLayout { get; }
    }
}