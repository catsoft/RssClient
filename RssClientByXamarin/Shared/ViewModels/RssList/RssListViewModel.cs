using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Droid.Repositories.Configuration;
using DynamicData;
using JetBrains.Annotations;
using ReactiveUI;
using Shared.Configuration.Settings;
using Shared.Extensions;
using Shared.Infrastructure.Dialogs;
using Shared.Infrastructure.Navigation;
using Shared.Infrastructure.ViewModels;
using Shared.Services.Rss;
using Shared.ViewModels.RssAllMessages;
using Shared.ViewModels.RssCreate;
using Shared.ViewModels.RssListEdit;

namespace Shared.ViewModels.RssList
{
    public class RssListViewModel : ViewModel
    {
        [NotNull] private readonly INavigator _navigator;
        [NotNull] private readonly IConfigurationRepository _configurationRepository;
        [NotNull] private readonly IRssService _rssService;

        public RssListViewModel(
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
            RssViewModel = new RssViewModel(rssService, dialogService, navigator, ListViewModel.SourceList);
            
            OpenCreateScreenCommand = ReactiveCommand.Create(DoOpenCreateScreen).NotNull();
            OpenAllMessagesScreenCommand = ReactiveCommand.Create(DoOpenAllMessagesScreen).NotNull();
            OpenListEditScreenCommand = ReactiveCommand.Create(DoOpenListEditScreen).NotNull();
            AllUpdateCommand = ReactiveCommand.CreateFromTask<IChangeSet<RssServiceModel>>(DoAllUpdate).NotNull();
        }

        [NotNull] public ListViewModel<RssServiceModel> ListViewModel { get; }
        
        [NotNull] public RssViewModel RssViewModel { get; }

        [NotNull] public ReactiveCommand<Unit, Unit> OpenCreateScreenCommand { get; }

        [NotNull] public ReactiveCommand<Unit, Unit> OpenAllMessagesScreenCommand { get; }

        [NotNull] public ReactiveCommand<Unit, Unit> OpenListEditScreenCommand { get; }

        [NotNull] public ReactiveCommand<Unit, IEnumerable<RssServiceModel>> GetListCommand { get; }

        [NotNull] public ReactiveCommand<IChangeSet<RssServiceModel>, Unit> AllUpdateCommand { get; }

        [NotNull] public AppConfiguration AppConfiguration => _configurationRepository.GetSettings<AppConfiguration>();

        private void DoOpenCreateScreen()
        {
            var way = App.Container.Resolve<IWay<RssCreateViewModel>>().NotNull();
            _navigator.Go(way);
        }

        private void DoOpenAllMessagesScreen()
        {
            var way = App.Container.Resolve<IWay<RssAllMessagesViewModel>>().NotNull();
            _navigator.Go(way);
        }

        private void DoOpenListEditScreen()
        {
            var way = App.Container.Resolve<IWay<RssListEditViewModel>>().NotNull();
            _navigator.Go(way);
        }
        
        [NotNull]
        private async Task DoAllUpdate([CanBeNull] IChangeSet<RssServiceModel> changes, CancellationToken token)
        {
            var updatable = ListViewModel.SourceList.Items?.Where(w => w != null)
                                .Where(w => !w.UpdateTime.HasValue || w.UpdateTime.Value.AddMinutes(5) > DateTimeOffset.Now)
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
