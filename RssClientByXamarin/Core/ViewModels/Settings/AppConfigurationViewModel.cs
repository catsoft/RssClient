using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Core.Configuration.Settings;
using Core.Infrastructure.ViewModels;
using Core.Repositories.Configurations;
using JetBrains.Annotations;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Core.ViewModels.Settings
{
    public class AppConfigurationViewModel : ViewModel
    {
        [NotNull] private readonly IConfigurationRepository _configurationRepository;
        [NotNull] private readonly ISubject<AppConfiguration> _appConfiguration = new Subject<AppConfiguration>();

        public AppConfigurationViewModel([NotNull] IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
            
            UpdateConfiguration = ReactiveCommand.Create<Action<AppConfiguration>>(DoUpdateConfiguration);

            _appConfiguration.AsObservable().ToPropertyEx(this, model => model.AppConfiguration);
            
            _appConfiguration.OnNext(GetAppConfiguration);
        }

        public AppConfiguration GetAppConfiguration => _configurationRepository.GetSettings<AppConfiguration>();
        
        public extern AppConfiguration AppConfiguration { [ObservableAsProperty] get; }

        public ReactiveCommand<Action<AppConfiguration>, Unit> UpdateConfiguration { get; }
        
        private void DoUpdateConfiguration(Action<AppConfiguration> action)
        {
            var appConfiguration = GetAppConfiguration;
            action?.Invoke(appConfiguration);
            _configurationRepository.SaveSetting(appConfiguration);
            _appConfiguration.OnNext(appConfiguration);
        }
    }
}