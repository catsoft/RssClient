using System;
using System.Reactive;
using Core.Configuration.Settings;
using Core.Infrastructure.ViewModels;
using Core.Repositories.Configurations;
using JetBrains.Annotations;
using ReactiveUI;

namespace Core.ViewModels.Settings.RssDetail
{
    public class SettingsRssDetailViewModel : ViewModel
    {
        public SettingsRssDetailViewModel([NotNull] IConfigurationRepository configurationRepository)
        {
            AppConfigurationViewModel = new AppConfigurationViewModel(configurationRepository);

            UpdateRssDetailCommand = ReactiveCommand.Create<MessagesViewer>(DoUpdateRssDetail);
        }

        [NotNull] public AppConfigurationViewModel AppConfigurationViewModel { get; }

        [NotNull] public ReactiveCommand<MessagesViewer, Unit> UpdateRssDetailCommand { get; }

        private void DoUpdateRssDetail(MessagesViewer messagesViewer)
        {
            AppConfigurationViewModel.UpdateConfiguration.Execute(config => config.MessagesViewer = messagesViewer).Subscribe();
        }
    }
}
