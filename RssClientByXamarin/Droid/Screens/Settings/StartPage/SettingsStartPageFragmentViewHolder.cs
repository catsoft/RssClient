using Android.Views;
using Android.Widget;
using Droid.NativeExtension;
using JetBrains.Annotations;

namespace Droid.Screens.Settings.StartPage
{
    public class SettingsStartPageFragmentViewHolder
    {
        public SettingsStartPageFragmentViewHolder([NotNull] View view)
        {
            RadioGroup = view.FindNotNull<RadioGroup>(Resource.Id.radioGroup_settingsStartPage_main);
            RssListRadioButton = view.FindNotNull<RadioButton>(Resource.Id.radioButton_settingsStartPage_rssList);
            AllMessagesRadioButton = view.FindNotNull<RadioButton>(Resource.Id.radioButton_settingsStartPage_messagesList);
        }
        
        [NotNull] public RadioGroup RadioGroup { get; }
        
        [NotNull] public RadioButton RssListRadioButton { get; }
        
        [NotNull] public RadioButton AllMessagesRadioButton { get; }
    }
}