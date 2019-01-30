using Android.OS;
using Android.Views;
using Android.Widget;
using Droid.Container;
using Droid.Repository;
using Droid.Screens.Navigation;
using Shared.Configuration;

namespace Droid.Screens.Settings
{
    public class SettingsThemeFragment : SubFragment, RadioGroup.IOnCheckedChangeListener
    {
        [Inject] 
        private IConfigurationRepository _configurationRepository;
        
        protected override int LayoutId => Resource.Layout.fragment_settings_theme;

        public SettingsThemeFragment()
        {
            
        }

        protected override void RestoreState(Bundle saved)
        {
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var appConfiguration = _configurationRepository.GetSettings<AppConfiguration>();
            var appTheme = appConfiguration.AppTheme;
            
            // TODO сделать один элемент с параметрами, устал уже так жить
            var radioGroup = view.FindViewById<RadioGroup>(Resource.Id.radioGroup_settingsTheme_main);
            var darkRadioButton = view.FindViewById<RadioButton>(Resource.Id.radioButton_settingsTheme_dark);
            var lightRadioButton = view.FindViewById<RadioButton>(Resource.Id.radioButton_settingsTheme_light);
            var defaultRadioButton = view.FindViewById<RadioButton>(Resource.Id.radioButton_settingsTheme_default);
            
            if (appTheme == AppTheme.Dark)
            {
                darkRadioButton.Checked = true;
            } else if (appTheme == AppTheme.Light)
            {
                lightRadioButton.Checked = true;
            } else if (appTheme == AppTheme.Default)
            {
                defaultRadioButton.Checked = true;
            }
            
            radioGroup.SetOnCheckedChangeListener(this);
            
            return view;
        }
        
        public void OnCheckedChanged(RadioGroup @group, int checkedId)
        {
            var appConfiguration = _configurationRepository.GetSettings<AppConfiguration>();

            var nextAppTheme = AppTheme.Light;
                
            if (checkedId == Resource.Id.radioButton_settingsTheme_dark)
            {
                nextAppTheme = AppTheme.Dark;
            } else if (checkedId == Resource.Id.radioButton_settingsTheme_light)
            {
                nextAppTheme = AppTheme.Light;
            } else if (checkedId == Resource.Id.radioButton_settingsTheme_default)
            {
                nextAppTheme = AppTheme.Default;
            }

            if (nextAppTheme != appConfiguration.AppTheme)
            {
                appConfiguration.AppTheme = nextAppTheme;

                _configurationRepository.SaveSetting(appConfiguration);

                Activity.Recreate();
            }
        }
    }
}