#region

using Android.OS;
using Android.Views;
using Android.Widget;
using Droid.Container;
using Droid.Repositories.Configuration;
using Droid.Screens.Navigation;
using Shared.Configuration.Settings;
using Shared.ViewModels.Settings;

#endregion

namespace Droid.Screens.Settings
{
    public class SettingsRssDetailFragment : BaseFragment<SettingsRssDetailViewModel>
    {
        [Inject] private IConfigurationRepository _configurationRepository;

        protected override int LayoutId => Resource.Layout.fragment_settings_rss_detail;

        public override bool IsRoot => false;

        protected override void RestoreState(Bundle saved) { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var radioGroup = view.FindViewById<RadioGroup>(Resource.Id.radioGroup_rss_detail_main);
            var inAppRadioButton = view.FindViewById<RadioButton>(Resource.Id.radioButton_settingsRssDetail_inApp);
            var inBrowserRadioButton =
                view.FindViewById<RadioButton>(Resource.Id.radioButton_settingsRssDetail_inBrowser);

            var configuration = _configurationRepository.GetSettings<AppConfiguration>();

            switch (configuration.MessagesViewer)
            {
                case MessagesViewer.App:
                    inAppRadioButton.Checked = true;
                    break;
                case MessagesViewer.Browser:
                    inBrowserRadioButton.Checked = true;
                    break;
            }

            radioGroup.CheckedChange += (sender, args) =>
            {
                var appConfiguration = _configurationRepository.GetSettings<AppConfiguration>();

                switch (args.CheckedId)
                {
                    case Resource.Id.radioButton_settingsRssDetail_inApp:
                        appConfiguration.MessagesViewer = MessagesViewer.App;
                        break;
                    case Resource.Id.radioButton_settingsRssDetail_inBrowser:
                        appConfiguration.MessagesViewer = MessagesViewer.Browser;
                        break;
                }

                _configurationRepository.SaveSetting(appConfiguration);
            };

            return view;
        }
    }
}
