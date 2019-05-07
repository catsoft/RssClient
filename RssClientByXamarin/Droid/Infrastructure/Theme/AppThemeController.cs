using System;
using Android.App;
using Core.Configuration.Settings;
using Core.Repositories.Configurations;

namespace Droid.Infrastructure.Theme
{
    public class AppThemeController
    {
        private readonly IConfigurationRepository _configurationRepository;

        public AppThemeController(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }

        public void SetTheme(Activity activity)
        {
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

            activity.SetTheme(themeId);
        }
    }
}
