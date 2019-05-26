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
using Core.ViewModels.RssFeeds.RssFeedsUpdater;
using DynamicData;
using JetBrains.Annotations;
using ReactiveUI;

namespace Core.ViewModels.RssFeeds.List
{
    public class RssFeedListViewModel : ViewModel
    {
        [NotNull] private readonly INavigator _navigator;
        [NotNull] private readonly IConfigurationRepository _configurationRepository;

        public RssFeedListViewModel(
            [NotNull] INavigator navigator,
            [NotNull] IConfigurationRepository configurationRepository,
            [NotNull] IRssFeedService rssFeedService,
            [NotNull] IDialogService dialogService,
            [NotNull] RssFeedsUpdaterViewModel rssFeedsUpdaterViewModel)
        {
            _navigator = navigator;
            _configurationRepository = configurationRepository;
            RssFeedsUpdaterViewModel = rssFeedsUpdaterViewModel;

            GetListCommand = ReactiveCommand.CreateFromTask(async token => await rssFeedService.GetListAsync(token)).NotNull();
            ListViewModel = new ListViewModel<RssFeedServiceModel>(GetListCommand);
            RssFeedItemViewModel = new RssFeedItemViewModel(rssFeedService, dialogService, navigator, ListViewModel.SourceList);

            OpenCreateScreenCommand = ReactiveCommand.Create(DoOpenCreateScreen).NotNull();
            OpenAllMessagesScreenCommand = ReactiveCommand.Create(DoOpenAllMessagesScreen).NotNull();
            OpenListEditScreenCommand = ReactiveCommand.Create(DoOpenListEditScreen).NotNull();

            var isNewMessages = ListViewModel.ConnectChanges
                .Select(w => ListViewModel.SourceList.Items.NotNull().Any(rss => rss.NotNull().CountNewMessages > 0))
                .ObserveOn(RxApp.MainThreadScheduler.NotNull());
            ReadAllMessagesCommand = ReactiveCommand.CreateFromTask(DoReadAllMessages, isNewMessages).NotNull();

            RssFeedsUpdaterViewModel.UpdatedRss.Subscribe(UpdateList);
        }

        [NotNull] public ListViewModel<RssFeedServiceModel> ListViewModel { get; }

        [NotNull] public RssFeedItemViewModel RssFeedItemViewModel { get; }

        [NotNull] public RssFeedsUpdaterViewModel RssFeedsUpdaterViewModel { get; }

        [NotNull] public ReactiveCommand<Unit, Unit> OpenCreateScreenCommand { get; }

        [NotNull] public ReactiveCommand<Unit, Unit> OpenAllMessagesScreenCommand { get; }

        [NotNull] public ReactiveCommand<Unit, Unit> OpenListEditScreenCommand { get; }

        [NotNull] public ReactiveCommand<Unit, Unit> ReadAllMessagesCommand { get; }

        [NotNull] public ReactiveCommand<Unit, IEnumerable<RssFeedServiceModel>> GetListCommand { get; }

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

        private Task DoReadAllMessages(CancellationToken arg)
        {
            return Task.Run(() =>
                {
                    var allRssFeeds = ListViewModel.SourceList.Items.NotNull().ToList();
                    foreach (var rssFeedServiceModel in allRssFeeds) 
                        RssFeedItemViewModel.ReadAllMessagesCommand.ExecuteIfCan(rssFeedServiceModel);
                },
                arg);
        }

        private void UpdateList([NotNull] RssFeedServiceModel model)
        {
            var item = ListViewModel.SourceList.Items?.Where(w => w != null).FirstOrDefault(w => w.Id == model.Id);
            if (item != null) ListViewModel.SourceList.Replace(item, model);
        }
    }
}