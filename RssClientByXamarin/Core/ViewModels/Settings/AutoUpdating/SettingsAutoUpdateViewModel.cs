using System;
using System.Reactive;
using System.Reactive.Linq;
using Core.Extensions;
using Core.Infrastructure.ViewModels;
using Core.Repositories.Configurations;
using JetBrains.Annotations;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Core.ViewModels.Settings.AutoUpdating
{
    public class SettingsAutoUpdateViewModel : ViewModel
    {
        public SettingsAutoUpdateViewModel([NotNull] IConfigurationRepository configurationRepository)
        {
            AppConfigurationViewModel = new AppConfigurationViewModel(configurationRepository);
            
            UpdateIsAutoUpdateCommand = ReactiveCommand.Create<bool>(DoUpdateIsAutoUpdate);
            
            UpdateAutoUpdateIntervalCommand = ReactiveCommand.Create<int>(DoUpdateAutoUpdateInterval);

            AppConfigurationViewModel.WhenAnyValue(w => w.AppConfiguration)
                .NotNull()
                .Select(w => w.AutoUpdateInterval / 1000 / 60)
                .Subscribe(w => Interval = w.ToString());

            this.WhenAnyValue(w => w.Interval)
                .Select(int.Parse)
                .Where(w => w != AppConfigurationViewModel.AppConfiguration.AutoUpdateInterval / 1000 / 60)
                .InvokeCommand(UpdateAutoUpdateIntervalCommand);
        }
        
        [NotNull] public AppConfigurationViewModel AppConfigurationViewModel { get; }
        
        [NotNull] public ReactiveCommand<bool, Unit> UpdateIsAutoUpdateCommand { get; }
        
        [Reactive]
        public string Interval { get; set; }
        
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