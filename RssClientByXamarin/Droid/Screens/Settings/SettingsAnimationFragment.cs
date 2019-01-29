using System.Collections.Generic;
using System.Linq;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Droid.Container;
using Droid.Repository;
using Droid.Screens.Navigation;
using Shared.Configuration;
using Shared.Utils;

namespace Droid.Screens.Settings
{
    public class SettingsAnimationFragment : SubFragment
    {
        [Inject]
        private IConfigurationRepository _configurationRepository;
        
        protected override int LayoutId => Resource.Layout.fragment_settings_animation;

        protected override void RestoreState(Bundle saved)
        {
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var appConfiguration = _configurationRepository.GetSettings<AppConfiguration>();
            
            var speedSpinner = view.FindViewById<AppCompatSpinner>(Resource.Id.appCompatSpinner_settingsStartPage_speedAnimation);
            var typeSpinner = view.FindViewById<AppCompatSpinner>(Resource.Id.appCompatSpinner_settingsStartPage_typeAnimation);

            var animationSpeeds = new List<AnimationSpeed>
            {
                AnimationSpeed.x0_25,
                AnimationSpeed.x0_5,
                AnimationSpeed.x,
                AnimationSpeed.x2,
                AnimationSpeed.x4,
                AnimationSpeed.x8
            };

            var animationTypes = new List<AnimationType>
            {
                AnimationType.None,
                AnimationType.Fade,
                AnimationType.From_left,
                AnimationType.From_right,
                AnimationType.From_bottom
            };
            
            speedSpinner.Adapter = new ArrayAdapter(Context, Resource.Layout.support_simple_spinner_dropdown_item,
                animationSpeeds.Select(w => w.ToLocaleString()).ToList());

            typeSpinner.Adapter = new ArrayAdapter(Context, Resource.Layout.support_simple_spinner_dropdown_item,
                animationTypes.Select(w => w.ToLocaleString()).ToList());

            speedSpinner.SetSelection(animationSpeeds.IndexOf(appConfiguration.AnimationSpeed));
            typeSpinner.SetSelection(animationTypes.IndexOf(appConfiguration.AnimationType));
            
            speedSpinner.ItemSelected += (sender, args) =>
            {
                var configuration = _configurationRepository.GetSettings<AppConfiguration>();
                configuration.AnimationSpeed = animationSpeeds[args.Position];
                _configurationRepository.SaveSetting(configuration);
            };

            typeSpinner.ItemSelected += (sender, args) =>
            {
                var configuration = _configurationRepository.GetSettings<AppConfiguration>();
                configuration.AnimationType= animationTypes[args.Position];
                _configurationRepository.SaveSetting(configuration);
            };
            
            return view;
        }
    }
}