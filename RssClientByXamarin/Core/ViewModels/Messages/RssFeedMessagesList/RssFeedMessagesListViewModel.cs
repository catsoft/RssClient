using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Extensions;
using Core.Infrastructure.Dialogs;
using Core.Infrastructure.Navigation;
using Core.Infrastructure.ViewModels;
using Core.Repositories.Configurations;
using Core.Services.RssFeeds;
using Core.Services.RssMessages;
using Core.ViewModels.Lists;
using Core.ViewModels.RssFeeds;
using Core.ViewModels.Settings;
using JetBrains.Annotations;
using ReactiveUI;

namespace Core.ViewModels.Messages.RssFeedMessagesList
{
    public class RssFeedMessagesListViewModel : ViewModelWithParameter<RssFeedMessagesListParameters>
    {
        [NotNull] private readonly IRssMessageService _rssMessageService;
        [NotNull] private readonly IRssFeedService _rssFeedService;

        public RssFeedMessagesListViewModel([NotNull] RssFeedMessagesListParameters parameters,
            [NotNull] IRssMessageService rssMessageService,
            [NotNull] INavigator navigator,
            [NotNull] IConfigurationRepository configurationRepository, 
            [NotNull] IRssFeedService rssFeedService,
            [NotNull] IDialogService dialogService) : base(parameters)
        {
            _rssMessageService = rssMessageService;
            _rssFeedService = rssFeedService;

            LoadCommand = ReactiveCommand.CreateFromTask(DoLoad).NotNull();
            RefreshCommand = ReactiveCommand.CreateFromTask(DoRefresh).NotNull();
            RefreshCommand.SelectUnit().InvokeCommand(LoadCommand);
            ListViewModel = new ListViewModel<RssMessageServiceModel>(LoadCommand);
            AppConfigurationViewModel = new AppConfigurationViewModel(configurationRepository);
            
            var isNewMessages = ListViewModel.ConnectChanges.Select(w => ListViewModel.SourceList.Items.Any(rss => !rss.IsRead)).ObserveOn(RxApp.MainThreadScheduler);
            ReadAllMessagesCommand = ReactiveCommand.CreateFromTask(DoReadAllMessagesCommand, isNewMessages);
            
            MessageItemViewModel = new MessageItemViewModel(rssMessageService, navigator, ListViewModel.SourceList, AppConfigurationViewModel);
            RssFeedItemViewModel = new RssFeedItemViewModel(rssFeedService, dialogService, navigator, null);

            RssFeedItemViewModel.DeleteItemCommand
                .SelectUnit()
                .Subscribe(w => navigator.GoBack());
        }

        [NotNull] public ListViewModel<RssMessageServiceModel> ListViewModel { get; }

        [NotNull] public MessageItemViewModel MessageItemViewModel { get; }
        
        [NotNull] public RssFeedItemViewModel RssFeedItemViewModel { get; }

        public AppConfigurationViewModel AppConfigurationViewModel { get; }
        
        [NotNull] public ReactiveCommand<Unit, IEnumerable<RssMessageServiceModel>> LoadCommand { get; }
        
        [NotNull] public ReactiveCommand<Unit, Unit> RefreshCommand { get; }
        
        [NotNull] public ReactiveCommand<Unit, Unit> ReadAllMessagesCommand { get; }

        private Task DoReadAllMessagesCommand(CancellationToken token)
        {
            return Task.Run(() =>
            {
                var allMessages = ListViewModel.SourceList.Items.Where(w => !w.IsRead).ToList();
                foreach (var rssMessageServiceModel in allMessages)
                {
                    MessageItemViewModel.ChangeReadItemCommand.ExecuteIfCan(rssMessageServiceModel);
                }
            }, token);
        }
        
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