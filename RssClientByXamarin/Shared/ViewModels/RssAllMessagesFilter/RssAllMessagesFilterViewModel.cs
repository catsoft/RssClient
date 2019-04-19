#region

using System.Reactive;
using Droid.Repositories.Configuration;
using JetBrains.Annotations;
using ReactiveUI;
using Shared.Configuration.Settings;
using Shared.Extensions;
using Shared.Infrastructure.Navigation;
using Shared.Infrastructure.ViewModels;

#endregion

namespace Shared.ViewModels.RssAllMessagesFilter
{
    public class RssAllMessagesFilterViewModel : ViewModel
    {
        [NotNull] private readonly IConfigurationRepository _configurationRepository;
        [NotNull] private readonly INavigator _navigator;

        public RssAllMessagesFilterViewModel([NotNull] IConfigurationRepository configurationRepository, [NotNull] INavigator navigator)
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
