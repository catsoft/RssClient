using System;
using System.Reactive;
using Core.Configuration.Settings;
using Core.Infrastructure.ViewModels;
using Core.Repositories.Configurations;
using JetBrains.Annotations;
using ReactiveUI;

namespace Core.ViewModels.Settings.Theme
{
    public class SettingsThemeViewModel : ViewModel
    {
        public SettingsThemeViewModel([NotNull] IConfigurationRepository configurationRepository)
        {
            AppConfigurationViewModel = new AppConfigurationViewModel(configurationRepository);
            
            UpdateAppThemeCommand = ReactiveCommand.Create<AppTheme>(DoUpdateAppTheme);
        }

        [NotNull] public AppConfigurationViewModel AppConfigurationViewModel { get; }
        
        [NotNull] public ReactiveCommand<AppTheme, Unit> UpdateAppThemeCommand { get; }
        
        private void DoUpdateAppTheme(AppTheme appTheme)
        {
            AppConfigurationViewModel.UpdateConfiguration.Execute(config => config.AppTheme = appTheme).Subscribe();
        }
    }
}
