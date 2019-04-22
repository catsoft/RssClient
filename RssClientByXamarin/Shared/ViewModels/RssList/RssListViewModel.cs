using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Droid.EmbeddedResourse;
using Droid.Repositories.Configuration;
using DynamicData;
using JetBrains.Annotations;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Shared.Configuration.Settings;
using Shared.Extensions;
using Shared.Infrastructure.Dialogs;
using Shared.Infrastructure.Navigation;
using Shared.Infrastructure.ViewModels;
using Shared.Services.Rss;
using Shared.ViewModels.RssAllMessages;
using Shared.ViewModels.RssCreate;
using Shared.ViewModels.RssEdit;
using Shared.ViewModels.RssItemDetail;
using Shared.ViewModels.RssListEdit;

namespace Shared.ViewModels.RssList
{
    public class RssListViewModel : ViewModel
    {
        [NotNull] private readonly IDialogService _dialogService;
        [NotNull] private readonly INavigator _navigator;
        [NotNull] private readonly IRssService _rssService;

        public RssListViewModel(
            [NotNull] INavigator navigator,
            [NotNull] IConfigurationRepository configurationRepository,
            [NotNull] IRssService rssService,
            [NotNull] IDialogService dialogService)
        {
            _navigator = navigator;
            _rssService = rssService;
            _dialogService = dialogService;

            SourceList = new SourceList<RssServiceModel>();
            SourceList.CountChanged.NotNull().Select(w => w == 0).ToPropertyEx(this, model => model.IsEmpty);

            OpenCreateScreenCommand = ReactiveCommand.Create(DoOpenCreateScreen).NotNull();
            OpenAllMessagesScreenCommand = ReactiveCommand.Create(DoOpenAllMessagesScreen).NotNull();
            OpenListEditScreenCommand = ReactiveCommand.Create(DoOpenListEditScreen).NotNull();
            GetListCommand = ReactiveCommand.CreateFromTask(async token => await _rssService.GetListAsync(token)).NotNull();
            GetListCommand.Subscribe(w =>
            {
                SourceList.Clear();
                SourceList.AddRange(w ?? new RssServiceModel[0]);
            });
            AllUpdateCommand = ReactiveCommand.CreateFromTask<IChangeSet<RssServiceModel>>(DoAllUpdate).NotNull();
            ConnectChanges().ObserveOn(RxApp.TaskpoolScheduler.NotNull()).InvokeCommand(AllUpdateCommand);

            OpenDetailScreenCommand = ReactiveCommand.Create<RssServiceModel>(DoOpenDetailScreen).NotNull();
            ShareItemCommand = ReactiveCommand.CreateFromTask<RssServiceModel>(DoItemShare).NotNull();
            ReadAllItemsCommand = ReactiveCommand.CreateFromTask<RssServiceModel>(DoReadAllItemMessage).NotNull();
            OpenEditItemScreenCommand = ReactiveCommand.Create<RssServiceModel>(DoOpenEditItemScreen).NotNull();
            ShowDeleteItemDialogCommand = ReactiveCommand.Create<RssServiceModel>(DoShowDeleteItemDialog).NotNull();
            ItemRemoveCommand = ReactiveCommand.CreateFromTask<RssServiceModel>(DoItemRemove).NotNull();

            AppConfiguration = configurationRepository.GetSettings<AppConfiguration>();
        }

        [NotNull] public ReactiveCommand<Unit, Unit> OpenCreateScreenCommand { get; }

        [NotNull] public ReactiveCommand<Unit, Unit> OpenAllMessagesScreenCommand { get; }

        [NotNull] public ReactiveCommand<Unit, Unit> OpenListEditScreenCommand { get; }

        [NotNull] public ReactiveCommand<Unit, IEnumerable<RssServiceModel>> GetListCommand { get; }

        [NotNull] public ReactiveCommand<IChangeSet<RssServiceModel>, Unit> AllUpdateCommand { get; }

        [NotNull] public ReactiveCommand<RssServiceModel, Unit> OpenDetailScreenCommand { get; }

        [NotNull] public ReactiveCommand<RssServiceModel, Unit> ShareItemCommand { get; }

        [NotNull] public ReactiveCommand<RssServiceModel, Unit> OpenEditItemScreenCommand { get; }

        [NotNull] public ReactiveCommand<RssServiceModel, Unit> ReadAllItemsCommand { get; }

        [NotNull] public ReactiveCommand<RssServiceModel, Unit> ShowDeleteItemDialogCommand { get; }

        [NotNull] public ReactiveCommand<RssServiceModel, Unit> ItemRemoveCommand { get; }

        [NotNull] public SourceList<RssServiceModel> SourceList { get; }

        public extern bool IsEmpty { [ObservableAsProperty] get; }

        [NotNull] public AppConfiguration AppConfiguration { get; }

        [NotNull]
        public IObservable<IChangeSet<RssServiceModel>> ConnectChanges() { return SourceList.Connect().NotNull(); }

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

        private void DoOpenDetailScreen([CanBeNull] RssServiceModel model)
        {
            var parameter = new RssMessagesListParameters(model);
            var typedParameter = new TypedParameter(parameter.GetType(), parameter);
            var way = App.Container.Resolve<IWayWithParameters<RssMessagesListViewModel, RssMessagesListParameters>>(typedParameter).NotNull();
            _navigator.Go(way);
        }

        private async Task DoReadAllItemMessage([CanBeNull] RssServiceModel model, CancellationToken token)
        {
            await _rssService.ReadAllMessagesAsync(model?.Id, token);
            SourceList.ReplaceAt(SourceList.Items.IndexOf(model), model);
        }

        private void DoOpenEditItemScreen([CanBeNull] RssServiceModel model)
        {
            var parameter = new RssEditParameters(model?.Id);
            var typedParameter = new TypedParameter(parameter.GetType(), parameter);
            var editWay = App.Container.Resolve<IWayWithParameters<RssEditViewModel, RssEditParameters>>(typedParameter).NotNull();
            _navigator.Go(editWay);
        }

        private void DoShowDeleteItemDialog([CanBeNull] RssServiceModel model)
        {
            _dialogService.ShowYesNoDialog(Strings.RssDeleteDialogTitle,
                "",
                Strings.Yes,
                Strings.No,
                async () => await DoItemRemove(model),
                null);
        }

        [NotNull]
        private async Task DoItemRemove([CanBeNull] RssServiceModel model)
        {
            SourceList.Remove(model);
            await _rssService.RemoveAsync(model?.Id);
        }

        [NotNull]
        private async Task DoItemShare([CanBeNull] RssServiceModel model, CancellationToken token)
        {
            await _rssService.ShareAsync(model, token);
        }
        
        [NotNull]
        private async Task DoAllUpdate([CanBeNull] IChangeSet<RssServiceModel> changes, CancellationToken token)
        {
            var updatable = SourceList.Items?.Where(w => w != null)
                                .Where(w => !w.UpdateTime.HasValue || w.UpdateTime.Value.AddMinutes(5) > DateTimeOffset.Now)
                                .ToList() ??
                            new List<RssServiceModel>();

            foreach (var rssServiceModel in updatable)
            {
                await _rssService.LoadAndUpdateAsync(rssServiceModel.Id, token);
                var newItem = await _rssService.GetAsync(rssServiceModel.Id, token);

                if (SourceList.Items?.Contains(rssServiceModel) == true) SourceList.Replace(rssServiceModel, newItem);
            }
        }
    }
}
