using Android.Views;
using Android.Widget;
using Core.Extensions;
using JetBrains.Annotations;

namespace Droid.Screens.Settings
{
    public class SettingsFragmentViewHolder
    {
        public SettingsFragmentViewHolder([NotNull] View view)
        {
            ScrollView = view.FindViewById<ScrollView>(Resource.Id.scrollView_settings_main).NotNull();
        }
        
        [NotNull] public ScrollView ScrollView { get; }
    }
}