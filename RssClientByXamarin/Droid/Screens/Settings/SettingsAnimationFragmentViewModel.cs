using Android.Views;
using Android.Widget;
using Core.Extensions;
using JetBrains.Annotations;

namespace Droid.Screens.Settings
{
    public class SettingsAnimationFragmentViewModel
    {
        public SettingsAnimationFragmentViewModel(View view)
        {
            GoToCustomizeButton = view.FindViewById<Button>(Resource.Id.button_animationSetting_gotoWeaver).NotNull();
        }
        
        [NotNull] public Button GoToCustomizeButton { get; }
    }
}