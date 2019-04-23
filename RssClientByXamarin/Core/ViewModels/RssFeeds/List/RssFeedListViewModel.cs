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
using Core.Repositories.Configuration;
using Core.Services.Rss;
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
        [NotNull] private readonly IRssService _rssService;

        public RssFeedListViewModel(
            [NotNull] INavigator navigator,
            [NotNull] IConfigurationRepository configurationRepository,
            [NotNull] IRssService rssService,
            [NotNull] IDialogService dialogService)
        {
            _navigator = navigator;
            _configurationRepository = configurationRepository;
            _rssService = rssService;

            GetListCommand = ReactiveCommand.CreateFromTask(async token => await _rssService.GetListAsync(token)).NotNull();
            ListViewModel = new ListViewModel<RssServiceModel>(GetListCommand);
            RssFeedItemViewModel = new RssFeedItemViewModel(rssService, dialogService, navigator, ListViewModel.SourceList);
            
            OpenCreateScreenCommand = ReactiveCommand.Create(DoOpenCreateScreen).NotNull();
            OpenAllMessagesScreenCommand = ReactiveCommand.Create(DoOpenAllMessagesScreen).NotNull();
            OpenListEditScreenCommand = ReactiveCommand.Create(DoOpenListEditScreen).NotNull();
            AllUpdateCommand = ReactiveCommand.CreateFromTask<IChangeSet<RssServiceModel>>(DoAllUpdate).NotNull();
            ListViewModel.ConnectChanges.ObserveOn(RxApp.TaskpoolScheduler.NotNull()).InvokeCommand(AllUpdateCommand);
        }

        [NotNull] public ListViewModel<RssServiceModel> ListViewModel { get; }
        
        [NotNull] public RssFeedItemViewModel RssFeedItemViewModel { get; }

        [NotNull] public ReactiveCommand<Unit, Unit> OpenCreateScreenCommand { get; }

        [NotNull] public ReactiveCommand<Unit, Unit> OpenAllMessagesScreenCommand { get; }

        [NotNull] public ReactiveCommand<Unit, Unit> OpenListEditScreenCommand { get; }

        [NotNull] public ReactiveCommand<Unit, IEnumerable<RssServiceModel>> GetListCommand { get; }

        [NotNull] public ReactiveCommand<IChangeSet<RssServiceModel>, Unit> AllUpdateCommand { get; }

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
        private async Task DoAllUpdate([CanBeNull] IChangeSet<RssServiceModel> changes, CancellationToken token)
        {
            var updatable = ListViewModel.SourceList.Items?.Where(w => w != null)
                                .Where(w => !w.UpdateTime.HasValue || w.UpdateTime.Value.AddMinutes(5) < DateTimeOffset.Now)
                                .ToList() ??
                            new List<RssServiceModel>();

            foreach (var rssServiceModel in updatable)
            {
                await _rssService.LoadAndUpdateAsync(rssServiceModel.Id, token);
                var newItem = await _rssService.GetAsync(rssServiceModel.Id, token);

                if (ListViewModel.SourceList.Items?.Contains(rssServiceModel) == true) 
                    ListViewModel.SourceList.Replace(rssServiceModel, newItem);
            }
        }
    }
}
