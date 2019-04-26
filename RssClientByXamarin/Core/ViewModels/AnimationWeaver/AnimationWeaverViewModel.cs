using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Core.Configuration.Settings;
using Core.Infrastructure.ViewModels;
using Core.Repositories.Configurations;
using Core.Resources;
using Core.Utils;
using JetBrains.Annotations;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Core.ViewModels.AnimationWeaver
{
    public class AnimationWeaverViewModel : ViewModel
    {
        [NotNull] private readonly IConfigurationRepository _configurationRepository;
        [NotNull] private readonly ISubject<AppConfiguration> _appConfiguration = new Subject<AppConfiguration>();

        public AnimationWeaverViewModel([NotNull] IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;

            Title = Strings.AnimationWeaverTitle;
            
            Types = AnimationTypeExtension.GetAnimationTypes();
            Speeds = AnimationSpeedExtension.GetAnimationSpeeds();

            TypesLocalized = Types.Select(w => w.ToLocaleString()).ToList();
            SpeedLocalized = Speeds.Select(w => w.ToLocaleString()).ToList();

            AppConfigurationObservable = _appConfiguration.AsObservable();
            AppConfigurationObservable.Select(w => w.AnimationEnter).ToPropertyEx(this, model => model.EnterAnim);
            AppConfigurationObservable.Select(w => w.AnimationExit).ToPropertyEx(this, model => model.ExitAnim);
            AppConfigurationObservable.Select(w => w.AnimationSpeed).ToPropertyEx(this, model => model.AnimationSpeed);
            AppConfigurationObservable.Select(w => w.IsDelay).ToPropertyEx(this, model => model.IsDelay);
            _appConfiguration.OnNext(AppConfiguration);

            UpdateEnterAnimCommand = ReactiveCommand.Create<AnimationType>(DoUpdateEnterAnim);
            UpdateExitAnimCommand = ReactiveCommand.Create<AnimationType>(DoUpdateExitAnim);
            UpdateSpeedAnimCommand = ReactiveCommand.Create<AnimationSpeed>(DoUpdateSpeedAnim);
            UpdateIsDelayAnimCommand = ReactiveCommand.Create<bool>(DoUpdateIsDelayAnim);
        }

        public string Title { get; }
        
        public ReactiveCommand<AnimationType, Unit> UpdateEnterAnimCommand { get; }

        public ReactiveCommand<AnimationType, Unit> UpdateExitAnimCommand { get; }

        public ReactiveCommand<AnimationSpeed, Unit> UpdateSpeedAnimCommand { get; }

        public ReactiveCommand<bool, Unit> UpdateIsDelayAnimCommand { get; }

        [NotNull] public List<AnimationType> Types { get; }

        [NotNull] public List<string> TypesLocalized { get; }

        [NotNull] public List<AnimationSpeed> Speeds { get; }

        [NotNull] public List<string> SpeedLocalized { get; }

        public extern AnimationType EnterAnim { [ObservableAsProperty] get; }

        public extern AnimationType ExitAnim { [ObservableAsProperty] get; }

        public extern AnimationSpeed AnimationSpeed { [ObservableAsProperty] get; }

        public extern bool IsDelay { [ObservableAsProperty] get; }

        [NotNull] public AppConfiguration AppConfiguration => _configurationRepository.GetSettings<AppConfiguration>();

        [NotNull] public IObservable<AppConfiguration> AppConfigurationObservable { get; }

        private void DoUpdateEnterAnim(AnimationType enter)
        {
            UpdateConfiguration((config) => config.AnimationEnter = enter);
        }

        private void DoUpdateExitAnim(AnimationType exit)
        {
            UpdateConfiguration((config) => config.AnimationExit = exit);
        }

        private void DoUpdateSpeedAnim(AnimationSpeed speed)
        {
            UpdateConfiguration((config) => config.AnimationSpeed = speed);
        }

        private void DoUpdateIsDelayAnim(bool idDelay)
        {
            UpdateConfiguration((config) => config.IsDelay = idDelay);
        }

        private void UpdateConfiguration(Action<AppConfiguration> updateAction)
        {
            var appConfiguration = AppConfiguration;
            updateAction?.Invoke(appConfiguration);
            _configurationRepository.SaveSetting(appConfiguration);

            _appConfiguration.OnNext(appConfiguration);
        }
    }
}