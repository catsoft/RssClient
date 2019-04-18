using System.Reactive;
using Droid.Repository.Configuration;
using ReactiveUI;
using Shared.Configuration.Settings;
using Shared.Infrastructure.Navigation;
using Shared.Infrastructure.ViewModels;

namespace Shared.ViewModels.RssAllMessagesFilter
{
    public class RssAllMessagesFilterViewModel : ViewModel
    {
        private readonly IConfigurationRepository _configurationRepository;
        private readonly INavigator _navigator;
        
        public RssAllMessagesFilterViewModel(IConfigurationRepository configurationRepository, INavigator navigator)
        {
            _configurationRepository = configurationRepository;
            _navigator = navigator;
            
            ClearFilterCommand = ReactiveCommand.Create(DoClearFilter);
        }

        public ReactiveCommand<Unit, Unit> ClearFilterCommand { get; } 

        private void DoClearFilter()
        {
            _configurationRepository.DeleteSetting<AllMessageFilterConfiguration>();
         
            _navigator.GoBack();
        }
    }
}