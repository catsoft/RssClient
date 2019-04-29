using Android.Views;
using Android.Widget;
using Core.Extensions;
using JetBrains.Annotations;

namespace Droid.Screens.Settings.RssDetail
{
    public class SettingsRssDetailFragmentViewHolder
    {
        public SettingsRssDetailFragmentViewHolder([NotNull] View view)
        {
            RadioGroup = view.FindViewById<RadioGroup>(Resource.Id.radioGroup_rss_detail_main).NotNull();
            InAppRadioButton = view.FindViewById<RadioButton>(Resource.Id.radioButton_settingsRssDetail_inApp).NotNull();
            InBrowserRadioButton = view.FindViewById<RadioButton>(Resource.Id.radioButton_settingsRssDetail_inBrowser).NotNull();
        }
        
        [NotNull] public RadioGroup RadioGroup { get; }
        
        [NotNull] public RadioButton InAppRadioButton { get; }
        
        [NotNull] public RadioButton InBrowserRadioButton { get; }
    }
}