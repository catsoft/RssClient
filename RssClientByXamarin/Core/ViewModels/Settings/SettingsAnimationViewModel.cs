using System.Reactive;
using Autofac;
using Core.Extensions;
using Core.Infrastructure.Navigation;
using Core.Infrastructure.ViewModels;
using Core.ViewModels.AnimationWeaver;
using JetBrains.Annotations;
using ReactiveUI;

namespace Core.ViewModels.Settings
{
    public class SettingsAnimationViewModel : ViewModel
    {
        [NotNull] private readonly INavigator _navigator;

        public SettingsAnimationViewModel([NotNull] INavigator navigator)
        {
            _navigator = navigator;

            GoToWeaverCommand = ReactiveCommand.Create(DoGoToWeaver).NotNull();
        }

        [NotNull] public ReactiveCommandBase<Unit, Unit> GoToWeaverCommand { get; }
        
        
        private void DoGoToWeaver()
        {
            var way = App.Container.Resolve<IWay<AnimationWeaverViewModel>>().NotNull();
            _navigator.Go(way);
        }
    }
}
