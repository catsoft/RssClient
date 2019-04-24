using System.Collections.Generic;
using System.Linq;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Core.Configuration.Settings;
using Core.Repositories.Configurations;
using Core.Utils;
using Core.ViewModels.Settings;
using Droid.Container;
using Droid.Resources;
using Droid.Screens.Navigation;

namespace Droid.Screens.Settings
{
    public class SettingsAnimationFragment : BaseFragment<SettingsAnimationViewModel>
    {
        [Inject] private IConfigurationRepository _configurationRepository;

        protected override int LayoutId => Resource.Layout.fragment_settings_animation;

        public override bool IsRoot => false;

        protected override void RestoreState(Bundle saved) { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var appConfiguration = _configurationRepository.GetSettings<AppConfiguration>();

            var speedSpinner = view.FindViewById<AppCompatSpinner>(Resource.Id.appCompatSpinner_settingsStartPage_speedAnimation);
            var typeSpinner = view.FindViewById<AppCompatSpinner>(Resource.Id.appCompatSpinner_settingsStartPage_typeAnimation);

            var animationSpeeds = new List<AnimationSpeed>
            {
                AnimationSpeed.X025,
                AnimationSpeed.X033,
                AnimationSpeed.X05,
                AnimationSpeed.X,
                AnimationSpeed.X2,
                AnimationSpeed.X3,
                AnimationSpeed.X4,
                AnimationSpeed.Max
            };

            var animationTypes = new List<AnimationType>
            {
                AnimationType.None,
                AnimationType.OnlyFade,
                AnimationType.ExitFadeEnterFromBottom,
                AnimationType.ExitToBottomEnterFade,
                AnimationType.ExitToBottomEnterFromBottom,
                AnimationType.FromLeftToRight,
                AnimationType.FromRightToLeft
            };

            speedSpinner.Adapter = new ArrayAdapter(Context,
                Resource.Layout.support_simple_spinner_dropdown_item,
                animationSpeeds.Select(w => w.ToLocaleString()).ToList());

            typeSpinner.Adapter = new ArrayAdapter(Context,
                Resource.Layout.support_simple_spinner_dropdown_item,
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
                configuration.AnimationType = animationTypes[args.Position];
                _configurationRepository.SaveSetting(configuration);
            };

            return view;
        }
    }
}
