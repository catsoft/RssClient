using Android.Views;
using Android.Widget;
using Droid.NativeExtension;
using JetBrains.Annotations;

namespace Droid.Screens.Settings.RssDetail
{
    public class SettingsRssDetailFragmentViewHolder
    {
        public SettingsRssDetailFragmentViewHolder([NotNull] View view)
        {
            RadioGroup = view.FindNotNull<RadioGroup>(Resource.Id.radioGroup_rss_detail_main);
            InAppRadioButton = view.FindNotNull<RadioButton>(Resource.Id.radioButton_settingsRssDetail_inApp);
            InBrowserRadioButton = view.FindNotNull<RadioButton>(Resource.Id.radioButton_settingsRssDetail_inBrowser);
        }
        
        [NotNull] public RadioGroup RadioGroup { get; }
        
        [NotNull] public RadioButton InAppRadioButton { get; }
        
        [NotNull] public RadioButton InBrowserRadioButton { get; }
    }
}