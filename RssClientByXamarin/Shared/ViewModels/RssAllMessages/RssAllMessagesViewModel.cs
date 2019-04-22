using System.Collections.Generic;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Droid.Repositories.Configuration;
using JetBrains.Annotations;
using ReactiveUI;
using Shared.Configuration.Settings;
using Shared.Database.Rss;
using Shared.Extensions;
using Shared.Infrastructure.Navigation;
using Shared.Infrastructure.ViewModels;
using Shared.ViewModels.RssAllMessagesFilter;
using Shared.ViewModels.RssCreate;
using Shared.ViewModels.RssList;

namespace Shared.ViewModels.RssAllMessages
{
    public class RssAllMessagesViewModel : ViewModel
    {
        [NotNull] private readonly INavigator _navigator;
        [NotNull] private readonly IRssMessageService _rssMessageService;
        [NotNull] private readonly IConfigurationRepository _configurationRepository;

        public RssAllMessagesViewModel([NotNull] INavigator navigator,
            [NotNull] IRssMessageService rssMessageService,
            [NotNull] IConfigurationRepository configurationRepository)
        {
            _navigator = navigator;
            _rssMessageService = rssMessageService;
            _configurationRepository = configurationRepository;

            LoadRssMessagesCommand = ReactiveCommand.CreateFromTask(DoLoadRssMessages).NotNull();
            ListViewModel = new ListViewModel<RssMessageServiceModel>(LoadRssMessagesCommand);
            MessageViewModel = new RssListMessageViewModel(rssMessageService, navigator, ListViewModel.SourceList);
            
            OpenCreateScreenCommand = ReactiveCommand.Create(DoOpenCreateScreen).NotNull();
            OpenRssListScreenCommand = ReactiveCommand.Create(DoOpenRssListScreen).NotNull();
            OpenRssAllMessagesFilterScreenCommand = ReactiveCommand.Create(DoOpenAllMessagesFilterScreen).NotNull();
        }

        [NotNull] public ListViewModel<RssMessageServiceModel> ListViewModel { get; }
        
        [NotNull] public RssListMessageViewModel MessageViewModel { get; }
        
        [NotNull] public ReactiveCommand<Unit, Unit> OpenCreateScreenCommand { get; }

        [NotNull] public ReactiveCommand<Unit, Unit> OpenRssListScreenCommand { get; }
        
        [NotNull] public ReactiveCommand<Unit, Unit> OpenRssAllMessagesFilterScreenCommand { get; }
        
        [NotNull] public ReactiveCommand<Unit, IEnumerable<RssMessageServiceModel>> LoadRssMessagesCommand { get; }
        
        [NotNull]
        public AllMessageFilterConfiguration AllMessageFilterConfiguration => _configurationRepository.GetSettings<AllMessageFilterConfiguration>();
        
        [NotNull] public AppConfiguration AppConfiguration => _configurationRepository.GetSettings<AppConfiguration>();

        private void DoOpenCreateScreen() { _navigator.Go(App.Container.Resolve<IWay<RssCreateViewModel>>().NotNull()); }

        private void DoOpenRssListScreen() { _navigator.Go(App.Container.Resolve<IWay<RssListViewModel>>().NotNull()); }
    
        private void DoOpenAllMessagesFilterScreen() { _navigator.Go(App.Container.Resolve<IWay<RssAllMessagesFilterViewModel>>().NotNull()); }

        [NotNull]
        [ItemNotNull]
        private Task<IEnumerable<RssMessageServiceModel>> DoLoadRssMessages(CancellationToken token)
        {
            return _rssMessageService.GetAllFilterMessages(AllMessageFilterConfiguration, token);
        }
    }
}
