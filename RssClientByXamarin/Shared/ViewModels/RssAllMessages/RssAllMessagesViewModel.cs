using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Droid.Repositories.Configuration;
using DynamicData;
using JetBrains.Annotations;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Shared.Configuration.Settings;
using Shared.Database.Rss;
using Shared.Extensions;
using Shared.Infrastructure.Navigation;
using Shared.Infrastructure.ViewModels;
using Shared.ViewModels.RssAllMessagesFilter;
using Shared.ViewModels.RssCreate;
using Shared.ViewModels.RssList;
using Shared.ViewModels.RssMessage;

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

            SourceList = new SourceList<RssMessageServiceModel>();
            SourceList.CountChanged.NotNull().Select(w => w == 0).ToPropertyEx(this, model => model.IsEmpty);

            OpenCreateScreenCommand = ReactiveCommand.Create(DoOpenCreateScreen).NotNull();
            OpenRssListScreenCommand = ReactiveCommand.Create(DoOpenRssListScreen).NotNull();
            OpenRssAllMessagesFilterScreenCommand = ReactiveCommand.Create(DoOpenAllMessagesFilterScreen).NotNull();

            LoadRssMessagesCommand = ReactiveCommand.CreateFromTask(DoLoadRssMessages).NotNull();
            LoadRssMessagesCommand.Subscribe(w =>
            {
                SourceList.Clear();
                SourceList.AddRange(w);
            });
            
            OpenContentScreenCommand = ReactiveCommand.CreateFromTask<RssMessageServiceModel>(DoOpenContentScreen).NotNull();
            ChangeReadItemCommand = ReactiveCommand.CreateFromTask<RssMessageServiceModel>(DoChangeReadItem).NotNull();
            ChangeFavoriteCommand = ReactiveCommand.CreateFromTask<RssMessageServiceModel>(DoChangeFavoriteItem).NotNull();
            ShareItemCommand = ReactiveCommand.CreateFromTask<RssMessageServiceModel>(DoShareItem).NotNull();
        }

        [NotNull] public SourceList<RssMessageServiceModel> SourceList { get; }

        [NotNull] public IObservable<IChangeSet<RssMessageServiceModel>> ConnectChanges => SourceList.Connect().NotNull();

        [NotNull] public ReactiveCommand<Unit, Unit> OpenCreateScreenCommand { get; }

        [NotNull] public ReactiveCommand<Unit, Unit> OpenRssListScreenCommand { get; }
        
        [NotNull] public ReactiveCommand<Unit, Unit> OpenRssAllMessagesFilterScreenCommand { get; }
        
        [NotNull] public ReactiveCommand<Unit, IEnumerable<RssMessageServiceModel>> LoadRssMessagesCommand { get; }
        
        [NotNull] public ReactiveCommand<RssMessageServiceModel, Unit> OpenContentScreenCommand { get; }
        
        [NotNull] public ReactiveCommand<RssMessageServiceModel, Unit> ChangeReadItemCommand { get; }
        
        [NotNull] public ReactiveCommand<RssMessageServiceModel, Unit> ChangeFavoriteCommand { get; }
        
        [NotNull] public ReactiveCommand<RssMessageServiceModel, Unit> ShareItemCommand { get; }
        
        public extern bool IsEmpty { [ObservableAsProperty] get; }

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

        [NotNull]
        private async Task DoOpenContentScreen([NotNull] RssMessageServiceModel model, CancellationToken token)
        {
            var parameter = new RssMessageParameters(model);
            var typedParameter = new TypedParameter(parameter.GetType(), parameter);
            var way = App.Container.Resolve<IWayWithParameters<RssMessageViewModel, RssMessageParameters>>(typedParameter).NotNull();
            _navigator.Go(way);

            if (!model.IsRead)
            {
                model.IsRead = true;
                SourceList.Replace(model, model);
                await _rssMessageService.UpdateAsync(model, token);
            }
        }

        [NotNull]
        private async Task DoChangeReadItem([NotNull] RssMessageServiceModel model, CancellationToken token)
        {
            model.IsRead = !model.IsRead;
            SourceList.Replace(model, model);
            await _rssMessageService.UpdateAsync(model, token);
        }
        
        [NotNull]
        private async Task DoChangeFavoriteItem([NotNull] RssMessageServiceModel model, CancellationToken token)
        {
            model.IsFavorite = !model.IsFavorite;
            SourceList.Replace(model, model);
            await _rssMessageService.UpdateAsync(model, token);
        }

        [NotNull]
        private async Task DoShareItem(RssMessageServiceModel model, CancellationToken token)
        {
            await _rssMessageService.ShareAsync(model, token);
        }
    }
}
