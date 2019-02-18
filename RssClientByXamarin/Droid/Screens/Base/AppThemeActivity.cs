using System;
using Android.OS;
using Droid.Container;
using Droid.Repository.Configuration;
using Shared.Configuration.Settings;

namespace Droid.Screens.Base
{
    public abstract class AppThemeActivity : InjectActivity
    {
        [Inject] private IConfigurationRepository _configurationRepository;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var appConfiguration = _configurationRepository.GetSettings<AppConfiguration>();
            var appTheme = appConfiguration.AppTheme;

            int themeId;

            switch (appTheme)
            {
                case AppTheme.Light:
                    themeId = Resource.Style.AppTheme_Light_NoActionBar;
                    break;
                case AppTheme.Dark:
                    themeId = Resource.Style.AppTheme_Dark_NoActionBar;
                    break;
                case AppTheme.Default:
                    themeId = Resource.Style.AppTheme_Default_NoActionBar;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            SetTheme(themeId);
        }
    }
}