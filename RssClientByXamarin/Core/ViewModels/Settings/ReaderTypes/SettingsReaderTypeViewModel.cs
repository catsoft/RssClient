using System.Reactive;
using Core.Configuration.Settings;
using Core.Extensions;
using Core.Infrastructure.ViewModels;
using Core.Repositories.Configurations;
using DynamicData.Annotations;
using ReactiveUI;

namespace Core.ViewModels.Settings.ReaderTypes
{
    public class SettingsReaderTypeViewModel : ViewModel
    {
        public SettingsReaderTypeViewModel([NotNull] IConfigurationRepository configurationRepository)
        {
            AppConfigurationViewModel = new AppConfigurationViewModel(configurationRepository);
            
            UpdateReaderTypeCommand = ReactiveCommand.Create<ReaderType>(DoUpdateReaderType);
        }

        [NotNull] public AppConfigurationViewModel AppConfigurationViewModel { get; }
        
        [NotNull] public ReactiveCommand<ReaderType, Unit> UpdateReaderTypeCommand { get; }
        
        private void DoUpdateReaderType(ReaderType value)
        {
            AppConfigurationViewModel.UpdateConfiguration.ExecuteIfCan(config => config.ReaderType = value);
        }
    }
}