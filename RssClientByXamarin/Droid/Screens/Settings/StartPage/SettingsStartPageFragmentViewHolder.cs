using Android.Views;
using Android.Widget;
using Core.Extensions;
using JetBrains.Annotations;

namespace Droid.Screens.Settings.StartPage
{
    public class SettingsStartPageFragmentViewHolder
    {
        public SettingsStartPageFragmentViewHolder([NotNull] View view)
        {
            RadioGroup = view.FindViewById<RadioGroup>(Resource.Id.radioGroup_settingsStartPage_main).NotNull();
            RssListRadioButton = view.FindViewById<RadioButton>(Resource.Id.radioButton_settingsStartPage_rssList).NotNull();
            AllMessagesRadioButton = view.FindViewById<RadioButton>(Resource.Id.radioButton_settingsStartPage_messagesList).NotNull();
        }
        
        [NotNull] public RadioGroup RadioGroup { get; }
        
        [NotNull] public RadioButton RssListRadioButton { get; }
        
        [NotNull] public RadioButton AllMessagesRadioButton { get; }
    }
}