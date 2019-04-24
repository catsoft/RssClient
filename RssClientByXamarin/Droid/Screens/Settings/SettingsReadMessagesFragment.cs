using Android.OS;
using Android.Views;
using Android.Widget;
using Core.Configuration.Settings;
using Core.Repositories.Configurations;
using Core.ViewModels.Settings;
using Droid.Container;
using Droid.Resources;
using Droid.Screens.Navigation;

namespace Droid.Screens.Settings
{
    public class SettingsReadMessagesFragment : BaseFragment<SettingsReadMessagesViewModel>
    {
        [Inject] private IConfigurationRepository _configurationRepository;

        protected override int LayoutId => Resource.Layout.fragment_settings_read_messages;

        public override bool IsRoot => false;

        protected override void RestoreState(Bundle saved) { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var appConfiguration = _configurationRepository.GetSettings<AppConfiguration>();

            var checkBox = view.FindViewById<CheckBox>(Resource.Id.checkBox_ReadMessages_hide);

            checkBox.Checked = appConfiguration.HideReadMessages;

            checkBox.CheckedChange += CheckBoxOnCheckedChange;

            return view;
        }

        private void CheckBoxOnCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            var appConfiguration = _configurationRepository.GetSettings<AppConfiguration>();
            appConfiguration.HideReadMessages = e.IsChecked;
            _configurationRepository.SaveSetting(appConfiguration);
        }
    }
}
