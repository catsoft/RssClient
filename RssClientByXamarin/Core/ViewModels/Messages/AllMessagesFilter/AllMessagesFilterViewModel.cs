using System.Reactive;
using Core.Configuration.AllMessageFilter;
using Core.Extensions;
using Core.Infrastructure.Navigation;
using Core.Infrastructure.ViewModels;
using Core.Repositories.Configurations;
using JetBrains.Annotations;
using ReactiveUI;

namespace Core.ViewModels.Messages.AllMessagesFilter
{
    public class AllMessagesFilterViewModel : ViewModel
    {
        [NotNull] private readonly IConfigurationRepository _configurationRepository;
        [NotNull] private readonly INavigator _navigator;

        public AllMessagesFilterViewModel([NotNull] IConfigurationRepository configurationRepository, [NotNull] INavigator navigator)
        {
            _configurationRepository = configurationRepository;
            _navigator = navigator;

            ClearFilterCommand = ReactiveCommand.Create(DoClearFilter).NotNull();
        }

        [NotNull] public ReactiveCommand<Unit, Unit> ClearFilterCommand { get; }

        private void DoClearFilter()
        {
            _configurationRepository.DeleteSetting<AllMessageFilterConfiguration>();

            _navigator.GoBack();
        }
    }
}
