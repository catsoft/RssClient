using System.Reactive;
using Core.Extensions;
using Core.Infrastructure.ViewModels;
using Core.Repositories.Configurations;
using JetBrains.Annotations;
using ReactiveUI;

namespace Core.ViewModels.Settings.HideReadMessages
{
    public class SettingsHideReadMessagesViewModel : ViewModel
    {
        public SettingsHideReadMessagesViewModel([NotNull] IConfigurationRepository configurationRepository)
        {
            AppConfigurationViewModel = new AppConfigurationViewModel(configurationRepository);
            
            UpdateHideReadMessagesCommand = ReactiveCommand.Create<bool>(DoUpdateReadMessages);
        }

        [NotNull] public AppConfigurationViewModel AppConfigurationViewModel { get; }
        
        [NotNull] public ReactiveCommand<bool, Unit> UpdateHideReadMessagesCommand { get; }
        
        private void DoUpdateReadMessages(bool value)
        {
            AppConfigurationViewModel.UpdateConfiguration.ExecuteIfCan(config => config.HideReadMessages = value);
        }
    }
}
