using System;
using System.Reactive;
using Core.Infrastructure.ViewModels;
using Core.Repositories.Configurations;
using JetBrains.Annotations;
using ReactiveUI;

namespace Core.ViewModels.Settings.LoadImages
{
    public class SettingsLoadImagesViewModel : ViewModel
    {
        public SettingsLoadImagesViewModel([NotNull] IConfigurationRepository configurationRepository)
        {
            AppConfigurationViewModel = new AppConfigurationViewModel(configurationRepository);

            UpdateLoadAndShowImagesCommand = ReactiveCommand.Create<bool>(DoUpdateLoadAndShow);
        }

        [NotNull] public AppConfigurationViewModel AppConfigurationViewModel { get; }
        
        [NotNull] public ReactiveCommand<bool, Unit> UpdateLoadAndShowImagesCommand { get; }
        
        private void DoUpdateLoadAndShow(bool value)
        {
            AppConfigurationViewModel.UpdateConfiguration.Execute((config) => config.LoadAndShowImages = value).Subscribe();
        }
    }
}
