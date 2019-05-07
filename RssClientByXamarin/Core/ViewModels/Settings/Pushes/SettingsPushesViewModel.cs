using System;
using System.Reactive;
using Core.Infrastructure.ViewModels;
using Core.Repositories.Configurations;
using JetBrains.Annotations;
using ReactiveUI;

namespace Core.ViewModels.Settings.Pushes
{
    public class SettingsPushesViewModel : ViewModel
    {
        public SettingsPushesViewModel([NotNull] IConfigurationRepository configurationRepository)
        {
            AppConfigurationViewModel = new AppConfigurationViewModel(configurationRepository);
            
            UpdateIsShowPushesCommand = ReactiveCommand.Create<bool>(DoUpdateIsShowPushes);
        }
        
        [NotNull] public AppConfigurationViewModel AppConfigurationViewModel { get; }
        
        [NotNull] public ReactiveCommand<bool, Unit> UpdateIsShowPushesCommand { get; }
        
        private void DoUpdateIsShowPushes(bool value)
        {
            AppConfigurationViewModel?.UpdateConfiguration.Execute((config) => config.IsShowPush = value).Subscribe();
        }
    }
}