using Android.Views;
using Android.Widget;
using Droid.NativeExtension;
using JetBrains.Annotations;

namespace Droid.Screens.Settings.ReaderTypes
{
    public class SettingsReaderTypeFragmentViewHolder
    {
        public SettingsReaderTypeFragmentViewHolder([NotNull] View view)
        {
            MainRadioGroup = view.FindNotNull<RadioGroup>(Resource.Id.radioGroup_settingsReaderType_main);
            StripRadioButton = view.FindNotNull<RadioButton>(Resource.Id.radioButton_settingsReaderType_strip);
            BookRadioButton = view.FindNotNull<RadioButton>(Resource.Id.radioButton_settingsReaderType_book);
        }
        
        [NotNull] public RadioGroup MainRadioGroup { get; }
        
        [NotNull] public RadioButton StripRadioButton { get; }
        
        [NotNull] public RadioButton BookRadioButton { get; }
    }
}