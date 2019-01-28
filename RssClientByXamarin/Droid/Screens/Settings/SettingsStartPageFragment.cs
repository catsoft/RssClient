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
    public class SettingsStartPageFragment : SubFragment
    {
        [Inject]
        private IConfigurationRepository _configurationRepository;
        
        protected override int LayoutId => Resource.Layout.fragment_settings_start_page;

        public SettingsStartPageFragment()
        {
            
        }
        
        protected override void RestoreState(Bundle saved)
        {
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var radioGroup = view.FindViewById<RadioGroup>(Resource.Id.radioGroup_settingsStartPage_main);

            var configuration = _configurationRepository.GetSettings<AppConfiguration>();

            if (configuration.StartPage == StartPage.RssList)
            {
                radioGroup.CheckedRadioButtonId = Resource.Id.radioButton_settingsStartPage_rssList;
            } else if (configuration.StartPage == StartPage.AllMessages)
            {
                
            }
            
            return view;
        }
    }
}