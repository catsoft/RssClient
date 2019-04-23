using System.Collections.Generic;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Core.Configuration.AllMessageFilter;
using Core.Configuration.Settings;
using Core.Extensions;
using Core.Infrastructure.Navigation;
using Core.Infrastructure.ViewModels;
using Core.Repositories.Configuration;
using Core.Services.RssMessages;
using Core.ViewModels.Lists;
using Core.ViewModels.Messages.AllMessagesFilter;
using Core.ViewModels.RssFeeds.Create;
using Core.ViewModels.RssFeeds.List;
using JetBrains.Annotations;
using ReactiveUI;

namespace Core.ViewModels.Messages.AllMessages
{
    public class AllMessagesViewModel : ViewModel
    {
        [NotNull] private readonly INavigator _navigator;
        [NotNull] private readonly IRssMessageService _rssMessageService;
        [NotNull] private readonly IConfigurationRepository _configurationRepository;

        public AllMessagesViewModel([NotNull] INavigator navigator,
            [NotNull] IRssMessageService rssMessageService,
            [NotNull] IConfigurationRepository configurationRepository)
        {
            _navigator = navigator;
            _rssMessageService = rssMessageService;
            _configurationRepository = configurationRepository;

            LoadRssMessagesCommand = ReactiveCommand.CreateFromTask(DoLoadRssMessages).NotNull();
            ListViewModel = new ListViewModel<RssMessageServiceModel>(LoadRssMessagesCommand);
            MessageItemViewModel = new MessageItemViewModel(rssMessageService, navigator, ListViewModel.SourceList);
            
            OpenCreateScreenCommand = ReactiveCommand.Create(DoOpenCreateScreen).NotNull();
            OpenRssListScreenCommand = ReactiveCommand.Create(DoOpenRssListScreen).NotNull();
            OpenRssAllMessagesFilterScreenCommand = ReactiveCommand.Create(DoOpenAllMessagesFilterScreen).NotNull();
        }

        [NotNull] public ListViewModel<RssMessageServiceModel> ListViewModel { get; }
        
        [NotNull] public MessageItemViewModel MessageItemViewModel { get; }
        
        [NotNull] public ReactiveCommand<Unit, Unit> OpenCreateScreenCommand { get; }

        [NotNull] public ReactiveCommand<Unit, Unit> OpenRssListScreenCommand { get; }
        
        [NotNull] public ReactiveCommand<Unit, Unit> OpenRssAllMessagesFilterScreenCommand { get; }
        
        [NotNull] public ReactiveCommand<Unit, IEnumerable<RssMessageServiceModel>> LoadRssMessagesCommand { get; }
        
        [NotNull]
        public AllMessageFilterConfiguration AllMessageFilterConfiguration => _configurationRepository.GetSettings<AllMessageFilterConfiguration>();
        
        [NotNull] public AppConfiguration AppConfiguration => _configurationRepository.GetSettings<AppConfiguration>();

        private void DoOpenCreateScreen() { _navigator.Go(App.Container.Resolve<IWay<RssFeedCreateViewModel>>().NotNull()); }

        private void DoOpenRssListScreen() { _navigator.Go(App.Container.Resolve<IWay<RssFeedListViewModel>>().NotNull()); }
    
        private void DoOpenAllMessagesFilterScreen() { _navigator.Go(App.Container.Resolve<IWay<AllMessagesFilterViewModel>>().NotNull()); }

        [NotNull]
        [ItemNotNull]
        private Task<IEnumerable<RssMessageServiceModel>> DoLoadRssMessages(CancellationToken token)
        {
            return _rssMessageService.GetAllFilterMessages(AllMessageFilterConfiguration, token);
        }
    }
}
