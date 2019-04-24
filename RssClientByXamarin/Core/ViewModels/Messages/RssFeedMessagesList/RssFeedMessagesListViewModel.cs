using System;
using System.Collections.Generic;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using Core.Configuration.Settings;
using Core.Extensions;
using Core.Infrastructure.Dialogs;
using Core.Infrastructure.Navigation;
using Core.Infrastructure.ViewModels;
using Core.Repositories.Configurations;
using Core.Services.RssFeeds;
using Core.Services.RssMessages;
using Core.ViewModels.Lists;
using Core.ViewModels.RssFeeds;
using JetBrains.Annotations;
using ReactiveUI;

namespace Core.ViewModels.Messages.RssFeedMessagesList
{
    public class RssFeedMessagesListViewModel : ViewModelWithParameter<RssFeedMessagesListParameters>
    {
        [NotNull] private readonly IRssMessageService _rssMessageService;
        [NotNull] private readonly IConfigurationRepository _configurationRepository;
        [NotNull] private readonly IRssFeedService _rssFeedService;

        public RssFeedMessagesListViewModel([NotNull] RssFeedMessagesListParameters parameters,
            [NotNull] IRssMessageService rssMessageService,
            [NotNull] INavigator navigator,
            [NotNull] IConfigurationRepository configurationRepository, 
            [NotNull] IRssFeedService rssFeedService,
            [NotNull] IDialogService dialogService) : base(parameters)
        {
            _rssMessageService = rssMessageService;
            _configurationRepository = configurationRepository;
            _rssFeedService = rssFeedService;

            LoadCommand = ReactiveCommand.CreateFromTask(DoLoad).NotNull();
            RefreshCommand = ReactiveCommand.CreateFromTask(DoRefresh).NotNull();
            RefreshCommand.SelectUnit().InvokeCommand(LoadCommand);
            ListViewModel = new ListViewModel<RssMessageServiceModel>(LoadCommand);
            MessageItemViewModel = new MessageItemViewModel(rssMessageService, navigator, ListViewModel.SourceList);
            RssFeedItemViewModel = new RssFeedItemViewModel(rssFeedService, dialogService, navigator, null);

            RssFeedItemViewModel.DeleteItemCommand
                .SelectUnit()
                .Subscribe(w => navigator.GoBack());
        }

        [NotNull] public ListViewModel<RssMessageServiceModel> ListViewModel { get; }

        [NotNull] public MessageItemViewModel MessageItemViewModel { get; }
        
        [NotNull] public RssFeedItemViewModel RssFeedItemViewModel { get; }
        
        [NotNull] public ReactiveCommand<Unit, IEnumerable<RssMessageServiceModel>> LoadCommand { get; }
        
        [NotNull] public ReactiveCommand<Unit, Unit> RefreshCommand { get; }
        
        public AppConfiguration AppConfiguration => _configurationRepository.GetSettings<AppConfiguration>();

        private async Task<IEnumerable<RssMessageServiceModel>> DoLoad(CancellationToken token)
        {
            return await _rssMessageService.GetMessagesForRss(Parameters.RssFeedModel.Id, token);
        }

        private async Task DoRefresh(CancellationToken token)
        {
            await _rssFeedService.LoadAndUpdateAsync(Parameters.RssFeedModel.Id, token);
        }
    }
}