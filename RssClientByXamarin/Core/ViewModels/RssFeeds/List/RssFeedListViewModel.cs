using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Core.Configuration.Settings;
using Core.Extensions;
using Core.Infrastructure.Dialogs;
using Core.Infrastructure.Navigation;
using Core.Infrastructure.ViewModels;
using Core.Repositories.Configurations;
using Core.Services.RssFeeds;
using Core.ViewModels.Lists;
using Core.ViewModels.Messages.AllMessages;
using Core.ViewModels.RssFeeds.Create;
using Core.ViewModels.RssFeeds.EditableList;
using DynamicData;
using JetBrains.Annotations;
using ReactiveUI;

namespace Core.ViewModels.RssFeeds.List
{
    public class RssFeedListViewModel : ViewModel
    {
        [NotNull] private readonly INavigator _navigator;
        [NotNull] private readonly IConfigurationRepository _configurationRepository;
        [NotNull] private readonly IRssFeedService _rssFeedService;

        public RssFeedListViewModel(
            [NotNull] INavigator navigator,
            [NotNull] IConfigurationRepository configurationRepository,
            [NotNull] IRssFeedService rssFeedService,
            [NotNull] IDialogService dialogService)
        {
            _navigator = navigator;
            _configurationRepository = configurationRepository;
            _rssFeedService = rssFeedService;

            GetListCommand = ReactiveCommand.CreateFromTask(async token => await _rssFeedService.GetListAsync(token)).NotNull();
            ListViewModel = new ListViewModel<RssFeedServiceModel>(GetListCommand);
            RssFeedItemViewModel = new RssFeedItemViewModel(rssFeedService, dialogService, navigator, ListViewModel.SourceList);
            
            OpenCreateScreenCommand = ReactiveCommand.Create(DoOpenCreateScreen).NotNull();
            OpenAllMessagesScreenCommand = ReactiveCommand.Create(DoOpenAllMessagesScreen).NotNull();
            OpenListEditScreenCommand = ReactiveCommand.Create(DoOpenListEditScreen).NotNull();
            AllUpdateCommand = ReactiveCommand.CreateFromTask<IChangeSet<RssFeedServiceModel>>(DoAllUpdate).NotNull();
            ListViewModel.ConnectChanges.ObserveOn(RxApp.TaskpoolScheduler.NotNull()).InvokeCommand(AllUpdateCommand);
        }

        [NotNull] public ListViewModel<RssFeedServiceModel> ListViewModel { get; }
        
        [NotNull] public RssFeedItemViewModel RssFeedItemViewModel { get; }

        [NotNull] public ReactiveCommand<Unit, Unit> OpenCreateScreenCommand { get; }

        [NotNull] public ReactiveCommand<Unit, Unit> OpenAllMessagesScreenCommand { get; }

        [NotNull] public ReactiveCommand<Unit, Unit> OpenListEditScreenCommand { get; }

        [NotNull] public ReactiveCommand<Unit, IEnumerable<RssFeedServiceModel>> GetListCommand { get; }

        [NotNull] public ReactiveCommand<IChangeSet<RssFeedServiceModel>, Unit> AllUpdateCommand { get; }

        [NotNull] public AppConfiguration AppConfiguration => _configurationRepository.GetSettings<AppConfiguration>();

        private void DoOpenCreateScreen()
        {
            var way = App.Container.Resolve<IWay<RssFeedCreateViewModel>>().NotNull();
            _navigator.Go(way);
        }

        private void DoOpenAllMessagesScreen()
        {
            var way = App.Container.Resolve<IWay<AllMessagesViewModel>>().NotNull();
            _navigator.Go(way);
        }

        private void DoOpenListEditScreen()
        {
            var way = App.Container.Resolve<IWay<RssFeedEditableListViewModel>>().NotNull();
            _navigator.Go(way);
        }
        
        [NotNull]
        private async Task DoAllUpdate([CanBeNull] IChangeSet<RssFeedServiceModel> changes, CancellationToken token)
        {
            var updatable = ListViewModel.SourceList.Items?.Where(w => w != null)
                                .Where(w => !w.UpdateTime.HasValue || w.UpdateTime.Value.AddMinutes(5) < DateTimeOffset.Now)
                                .ToList() ??
                            new List<RssFeedServiceModel>();

            foreach (var rssServiceModel in updatable)
            {
                await _rssFeedService.LoadAndUpdateAsync(rssServiceModel.Id, token);
                var newItem = await _rssFeedService.GetAsync(rssServiceModel.Id, token);

                if (ListViewModel.SourceList.Items?.Contains(rssServiceModel) == true) 
                    ListViewModel.SourceList.Replace(rssServiceModel, newItem);
            }
        }
    }
}
