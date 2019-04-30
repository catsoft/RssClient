using Android.Views;
using Android.Widget;
using Droid.NativeExtension;
using JetBrains.Annotations;

namespace Droid.Screens.Settings.Theme
{
    public class SettingsThemeFragmentViewHolder
    {
        public SettingsThemeFragmentViewHolder([NotNull] View view)
        {
            RadioGroup = view.FindNotNull<RadioGroup>(Resource.Id.radioGroup_settingsTheme_main);
            DarkRadioButton = view.FindNotNull<RadioButton>(Resource.Id.radioButton_settingsTheme_dark);
            LightRadioButton = view.FindNotNull<RadioButton>(Resource.Id.radioButton_settingsTheme_light);
            DefaultRadioButton = view.FindNotNull<RadioButton>(Resource.Id.radioButton_settingsTheme_default);
        }
        
        [NotNull] public RadioGroup RadioGroup { get; }
        
        [NotNull] public RadioButton DarkRadioButton { get; }
        
        [NotNull] public RadioButton LightRadioButton { get; }
        
        [NotNull] public RadioButton DefaultRadioButton { get; }
    }
}