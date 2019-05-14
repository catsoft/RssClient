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
            QiwiTextView = view.FindNotNull<TextView>(Resource.Id.textView_donate_qiwi);
            CopyImageView = view.FindNotNull<ImageButton>(Resource.Id.imageView_donate_copy);
        }
        
        [NotNull] public LinearLayout PayContainerLinearLayout { get; }
        
        [NotNull] public TextView PriceTextView { get; }
        
        [NotNull] public TextView QiwiTextView { get; }
        
        [NotNull] public ImageButton CopyImageView { get; }
    }
}