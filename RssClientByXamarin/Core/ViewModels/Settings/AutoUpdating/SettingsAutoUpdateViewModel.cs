using System;
using System.Reactive;
using Core.Infrastructure.ViewModels;
using Core.Repositories.Configurations;
using JetBrains.Annotations;
using ReactiveUI;

namespace Core.ViewModels.Settings.AutoUpdating
{
    public class SettingsAutoUpdateViewModel : ViewModel
    {
        public SettingsAutoUpdateViewModel([NotNull] IConfigurationRepository configurationRepository)
        {
            AppConfigurationViewModel = new AppConfigurationViewModel(configurationRepository);
            
            UpdateIsAutoUpdateCommand = ReactiveCommand.Create<bool>(DoUpdateIsAutoUpdate);
            
            UpdateAutoUpdateIntervalCommand = ReactiveCommand.Create<int>(DoUpdateAutoUpdateInterval);
        }
        
        [NotNull] public AppConfigurationViewModel AppConfigurationViewModel { get; }
        
        [NotNull] public ReactiveCommand<bool, Unit> UpdateIsAutoUpdateCommand { get; }
        
        /// <summary>
        /// In minutes
        /// </summary>
        [NotNull] public ReactiveCommand<int, Unit> UpdateAutoUpdateIntervalCommand { get; }
        
        private void DoUpdateIsAutoUpdate(bool value)
        {
            AppConfigurationViewModel.UpdateConfiguration.Execute((config) => config.IsAutoUpdate = value).Subscribe();
        }
        
        private void DoUpdateAutoUpdateInterval(int value)
        {
            AppConfigurationViewModel.UpdateConfiguration.Execute((config) => config.AutoUpdateInterval = value * 1000 * 60).Subscribe();
        }
    }
}