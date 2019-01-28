using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using Droid.Container;
using Droid.Repository;
using Droid.Screens.Navigation;
using Shared.Configuration;

namespace Droid.Screens.Settings
{
    public class SettingsRssDetailFragment : SubFragment
    {
        [Inject]
        private IConfigurationRepository _configurationRepository;
        
        protected override int LayoutId => Resource.Layout.fragment_settings_rss_detail;

        public SettingsRssDetailFragment()
        {
            
        }
        
        protected override void RestoreState(Bundle saved)
        {
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var radioGroup = view.FindViewById<RadioGroup>(Resource.Id.radioGroup_rss_detail_main);
            var inAppRadioButton = view.FindViewById<RadioButton>(Resource.Id.radioButton_settingsRssDetail_inApp);
            var inBrowserRadioButton = view.FindViewById<RadioButton>(Resource.Id.radioButton_settingsRssDetail_inBrowser);
            
            var configuration = _configurationRepository.GetSettings<AppConfiguration>();

            if (configuration.MessagesViewer == MessagesViewer.App)
            {
                inAppRadioButton.Checked = true;
            } 
            else if (configuration.MessagesViewer == MessagesViewer.Browser)
            {
                inBrowserRadioButton.Checked = true;
            }
            
            radioGroup.CheckedChange += (sender, args) =>
            {
                var appConfiguration = _configurationRepository.GetSettings<AppConfiguration>();

                if (args.CheckedId == Resource.Id.radioButton_settingsRssDetail_inApp)
                {
                    appConfiguration.MessagesViewer = MessagesViewer.App;
                } 
                else if (args.CheckedId == Resource.Id.radioButton_settingsRssDetail_inBrowser)
                {
                    appConfiguration.MessagesViewer = MessagesViewer.Browser;
                }
                
                _configurationRepository.SaveSetting(appConfiguration);
            };
            
            return view;
        }
    }
}