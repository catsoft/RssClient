using Android.Views;
using Android.Widget;
using Droid.NativeExtension;
using JetBrains.Annotations;

namespace Droid.Screens.Settings
{
    public class SettingsFragmentViewHolder
    {
        public SettingsFragmentViewHolder([NotNull] View view)
        {
            ScrollView = view.FindNotNull<ScrollView>(Resource.Id.scrollView_settings_main);
        }
        
        [NotNull] public ScrollView ScrollView { get; }
    }
}