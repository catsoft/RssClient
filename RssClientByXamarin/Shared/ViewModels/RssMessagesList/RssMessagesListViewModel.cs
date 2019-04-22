using System;
using System.Collections.Generic;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using Droid.Repositories.Configuration;
using JetBrains.Annotations;
using ReactiveUI;
using Shared.Configuration.Settings;
using Shared.Database.Rss;
using Shared.Extensions;
using Shared.Infrastructure.Dialogs;
using Shared.Infrastructure.Navigation;
using Shared.Infrastructure.ViewModels;
using Shared.Services.Rss;
using Shared.ViewModels.RssAllMessages;

namespace Shared.ViewModels.RssItemDetail
{
    public class RssMessagesListViewModel : ViewModelWithParameter<RssMessagesListParameters>
    {
        [NotNull] private readonly IRssMessageService _rssMessageService;
        [NotNull] private readonly IConfigurationRepository _configurationRepository;
        [NotNull] private readonly IRssService _rssService;

        public RssMessagesListViewModel([NotNull] RssMessagesListParameters parameters,
            [NotNull] IRssMessageService rssMessageService,
            [NotNull] INavigator navigator,
            [NotNull] IConfigurationRepository configurationRepository, 
            [NotNull] IRssService rssService,
            [NotNull] IDialogService dialogService) : base(parameters)
        {
            _rssMessageService = rssMessageService;
            _configurationRepository = configurationRepository;
            _rssService = rssService;

            LoadCommand = ReactiveCommand.CreateFromTask(DoLoad).NotNull();
            RefreshCommand = ReactiveCommand.CreateFromTask(DoRefresh).NotNull();
            RefreshCommand.SelectUnit().InvokeCommand(LoadCommand);
            ListViewModel = new ListViewModel<RssMessageServiceModel>(LoadCommand);
            MessageViewModel = new RssListMessageViewModel(rssMessageService, navigator, ListViewModel.SourceList);
            RssViewModel = new RssViewModel(rssService, dialogService, navigator, null);

            RssViewModel.DeleteItemCommand
                .SelectUnit()
                .Subscribe(w => navigator.GoBack());
        }

        [NotNull] public ListViewModel<RssMessageServiceModel> ListViewModel { get; }

        [NotNull] public RssListMessageViewModel MessageViewModel { get; }
        
        [NotNull] public RssViewModel RssViewModel { get; }
        
        [NotNull] public ReactiveCommand<Unit, IEnumerable<RssMessageServiceModel>> LoadCommand { get; }
        
        [NotNull] public ReactiveCommand<Unit, Unit> RefreshCommand { get; }
        
        public AppConfiguration AppConfiguration => _configurationRepository.GetSettings<AppConfiguration>();

        private async Task<IEnumerable<RssMessageServiceModel>> DoLoad(CancellationToken token)
        {
            return await _rssMessageService.GetMessagesForRss(Parameters.RssModel.Id, token);
        }

        private async Task DoRefresh(CancellationToken token)
        {
            await _rssService.LoadAndUpdateAsync(Parameters.RssModel.Id, token);
        }
    }
}