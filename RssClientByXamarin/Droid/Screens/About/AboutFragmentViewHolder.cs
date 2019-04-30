using Android.Views;
using Android.Widget;
using Droid.NativeExtension;
using JetBrains.Annotations;

namespace Droid.Screens.About
{
    public class AboutFragmentViewHolder
    {
        public AboutFragmentViewHolder([NotNull] View view)
        {
            VersionTextView = view.FindNotNull<TextView>(Resource.Id.textView_about_version);
            OtherTextView = view.FindNotNull<TextView>(Resource.Id.textView_about_other);
            ProjectLinkTextView = view.FindNotNull<TextView>(Resource.Id.textView_about_projectLink);
        }
        
        [NotNull] public TextView VersionTextView { get; }
        
        [NotNull] public TextView OtherTextView { get; }
        
        [NotNull] public TextView ProjectLinkTextView { get; }
    }
}