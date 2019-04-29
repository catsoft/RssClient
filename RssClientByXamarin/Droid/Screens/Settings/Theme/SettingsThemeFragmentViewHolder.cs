using Android.Views;
using Android.Widget;
using Core.Extensions;
using JetBrains.Annotations;

namespace Droid.Screens.Settings.Theme
{
    public class SettingsThemeFragmentViewHolder
    {
        public SettingsThemeFragmentViewHolder([NotNull] View view)
        {
            RadioGroup = view.FindViewById<RadioGroup>(Resource.Id.radioGroup_settingsTheme_main).NotNull();
            DarkRadioButton = view.FindViewById<RadioButton>(Resource.Id.radioButton_settingsTheme_dark).NotNull();
            LightRadioButton = view.FindViewById<RadioButton>(Resource.Id.radioButton_settingsTheme_light).NotNull();
            DefaultRadioButton = view.FindViewById<RadioButton>(Resource.Id.radioButton_settingsTheme_default).NotNull();
        }
        
        [NotNull] public RadioGroup RadioGroup { get; }
        
        [NotNull] public RadioButton DarkRadioButton { get; }
        
        [NotNull] public RadioButton LightRadioButton { get; }
        
        [NotNull] public RadioButton DefaultRadioButton { get; }
    }
}