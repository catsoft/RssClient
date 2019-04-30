using Android.Views;
using Android.Widget;
using Core.Extensions;
using JetBrains.Annotations;

namespace Droid.Screens.About
{
    public class AboutFragmentViewHolder
    {
        public AboutFragmentViewHolder([NotNull] View view)
        {
            VersionTextView = view.FindViewById<TextView>(Resource.Id.textView_about_version).NotNull();
            OtherTextView = view.FindViewById<TextView>(Resource.Id.textView_about_other).NotNull();
            ProjectLinkTextView = view.FindViewById<TextView>(Resource.Id.textView_about_projectLink).NotNull();
        }
        
        [NotNull] public TextView VersionTextView { get; }
        
        [NotNull] public TextView OtherTextView { get; }
        
        [NotNull] public TextView ProjectLinkTextView { get; }
    }
}