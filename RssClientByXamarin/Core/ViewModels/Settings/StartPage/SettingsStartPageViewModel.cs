using System;
using System.Reactive;
using Core.Infrastructure.ViewModels;
using Core.Repositories.Configurations;
using JetBrains.Annotations;
using ReactiveUI;

namespace Core.ViewModels.Settings.StartPage
{
    public class SettingsStartPageViewModel : ViewModel
    {
        public SettingsStartPageViewModel([NotNull] IConfigurationRepository configurationRepository)
        {
            AppConfigurationViewModel = new AppConfigurationViewModel(configurationRepository);
            
            UpdateStartPageCommand = ReactiveCommand.Create<Configuration.Settings.StartPage>(DoUpdateStartPage);
        }

        public AppConfigurationViewModel AppConfigurationViewModel { get; }
        
        public ReactiveCommand<Configuration.Settings.StartPage, Unit> UpdateStartPageCommand { get; }
        
        private void DoUpdateStartPage(Configuration.Settings.StartPage startPage)
        {
            AppConfigurationViewModel.UpdateConfiguration.Execute(config => config.StartPage = startPage).Subscribe();
        }
    }
}
