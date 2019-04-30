using Android.Views;
using Android.Widget;
using Droid.NativeExtension;
using JetBrains.Annotations;

namespace Droid.Screens.Settings.Animations
{
    public class SettingsAnimationFragmentViewHolder
    {
        public SettingsAnimationFragmentViewHolder(View view)
        {
            GoToCustomizeButton = view.FindNotNull<Button>(Resource.Id.button_animationSetting_gotoWeaver);
        }
        
        [NotNull] public Button GoToCustomizeButton { get; }
    }
}