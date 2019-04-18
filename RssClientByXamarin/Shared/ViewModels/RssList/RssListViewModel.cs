using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Droid.EmbeddedResourse;
using Droid.Repository.Configuration;
using Droid.Screens.Navigation;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Shared.Configuration.Settings;
using Shared.Infrastructure.Dialogs;
using Shared.Infrastructure.Navigation;
using Shared.Infrastructure.ViewModels;
using Shared.Services.Rss;
using Shared.ViewModels.RssAllMessages;
using Shared.ViewModels.RssCreate;
using Shared.ViewModels.RssEdit;
using Shared.ViewModels.RssItemDetail;
using Shared.ViewModels.RssListEdit;
using Xamarin.Essentials;

namespace Shared.ViewModels.RssList
{
    public class RssListViewModel : ViewModel
    {
        private readonly INavigator _navigator;
        private readonly IConfigurationRepository _configurationRepository;
        private readonly IRssService _rssService;
        private readonly IDialogService _dialogService;

        public RssListViewModel(INavigator navigator, IConfigurationRepository configurationRepository,
            IRssService rssService, IDialogService dialogService)
        {
            _navigator = navigator;
            _configurationRepository = configurationRepository;
            _rssService = rssService;
            _dialogService = dialogService;

            SourceList = new SourceList<RssServiceModel>();
            SourceList.CountChanged.Select(w => w == 0).ToPropertyEx(this, model => model.IsEmpty);

            OpenCreateScreenCommand = ReactiveCommand.Create(DoOpenCreateScreen);
            OpenAllMessagesScreenCommand = ReactiveCommand.Create(DoOpenAllMessagesScreen);
            OpenListEditScreenCommand = ReactiveCommand.Create(DoOpenListEditScreen);
            GetListCommand = ReactiveCommand.CreateFromTask(async token => await _rssService.GetListAsync(token));
            GetListCommand.Subscribe(w =>
            {
                SourceList.Clear();
                SourceList.AddRange(w ?? new RssServiceModel[0]);
            });
            AllUpdateCommand = ReactiveCommand.CreateFromTask<IChangeSet<RssServiceModel>>(DoAllUpdate);
            ConnectChanges().ObserveOn(RxApp.TaskpoolScheduler).InvokeCommand(AllUpdateCommand);

            OpenDetailScreenCommand = ReactiveCommand.Create<RssServiceModel>(DoOpenDetailScreen);
            ShareItemCommand = ReactiveCommand.CreateFromTask<RssServiceModel>(async w => await Share.RequestAsync(w.Rss));
            ReadAllItemsCommand = ReactiveCommand.CreateFromTask<RssServiceModel>(DoReadAllItemMessage);
            OpenEditItemScreenCommand = ReactiveCommand.Create<RssServiceModel>(DoOpenEditItemScreen);
            ShowDeleteItemDialogCommand = ReactiveCommand.Create<RssServiceModel>(DoShowDeleteItemDialog);
            ItemRemoveCommand = ReactiveCommand.CreateFromTask<RssServiceModel>(DoItemRemove);

            AppConfiguration = _configurationRepository.GetSettings<AppConfiguration>();
        }

        public ReactiveCommand<Unit, Unit> OpenCreateScreenCommand { get; }

        public ReactiveCommand<Unit, Unit> OpenAllMessagesScreenCommand { get; }

        public ReactiveCommand<Unit, Unit> OpenListEditScreenCommand { get; }

        public ReactiveCommand<Unit, IEnumerable<RssServiceModel>> GetListCommand { get; }

        public ReactiveCommand<IChangeSet<RssServiceModel>, Unit> AllUpdateCommand { get; }

        public ReactiveCommand<RssServiceModel, Unit> OpenDetailScreenCommand { get; }

        public ReactiveCommand<RssServiceModel, Unit> ShareItemCommand { get; }

        public ReactiveCommand<RssServiceModel, Unit> OpenEditItemScreenCommand { get; }

        public ReactiveCommand<RssServiceModel, Unit> ReadAllItemsCommand { get; }

        public ReactiveCommand<RssServiceModel, Unit> ShowDeleteItemDialogCommand { get; }

        public ReactiveCommand<RssServiceModel, Unit> ItemRemoveCommand { get; }

        public SourceList<RssServiceModel> SourceList { get; }

        public IObservable<IChangeSet<RssServiceModel>> ConnectChanges() => SourceList.Connect();

        public extern bool IsEmpty { [ObservableAsProperty] get; }

        public AppConfiguration AppConfiguration { get; }

        private void DoOpenCreateScreen()
        {
            var way = App.Container.Resolve<IWay<RssCreateViewModel>>();
            _navigator.Go(way);
        }

        private void DoOpenAllMessagesScreen()
        {
            var way = App.Container.Resolve<IWay<RssAllMessagesViewModel>>();
            _navigator.Go(way);
        }

        private void DoOpenListEditScreen()
        {
            var way = App.Container.Resolve<IWay<RssListEditViewModel>>();
            _navigator.Go(way);
        }

        private void DoOpenDetailScreen(RssServiceModel model)
        {
            var parameter = new RssMessagesListParameters(model);
            var typedParameter = new TypedParameter(parameter.GetType(), parameter);
            var way =
                App.Container.Resolve<IWayWithParameters<RssMessagesListViewModel, RssMessagesListParameters>>(
                    typedParameter);
            _navigator.Go(way);
        }

        private async Task DoReadAllItemMessage(RssServiceModel model, CancellationToken token)
        {
            await _rssService.ReadAllMessagesAsync(model.Id, token);
            SourceList.ReplaceAt(SourceList.Items.IndexOf(model), model);
        }

        private void DoOpenEditItemScreen(RssServiceModel model)
        {
            var parameter = new RssEditParameters(model.Id);
            var typedParameter = new TypedParameter(parameter.GetType(), parameter);
            var editWay =
                App.Container.Resolve<IWayWithParameters<RssEditViewModel, RssEditParameters>>(typedParameter);
            _navigator.Go(editWay);
        }

        private void DoShowDeleteItemDialog(RssServiceModel model)
        {
            _dialogService.ShowYesNoDialog(Strings.RssDeleteDialogTitle, "", Strings.Yes, Strings.No,
                async () => { await DoItemRemove(model); }, null);
        }

        private async Task DoItemRemove(RssServiceModel model)
        {
            SourceList.Remove(model);
            await _rssService.RemoveAsync(model.Id);
        }

        private async Task DoAllUpdate(IChangeSet<RssServiceModel> changes, CancellationToken token)
        {
            var updatable = SourceList.Items.Where(w => w != null)
                       //TODO продумать условие
//                    ?.Where(w => !w.UpdateTime.HasValue || w.UpdateTime.Value.AddMinutes(5) > DateTimeOffset.Now)
                    .ToList() ?? new List<RssServiceModel>();
            
            foreach (var rssServiceModel in updatable)
            {
                await _rssService.LoadAndUpdateAsync(rssServiceModel.Id, token);
                var newItem = await _rssService.GetAsync(rssServiceModel.Id, token);

                if (SourceList.Items?.Contains(rssServiceModel) == true)
                {
                    SourceList.Replace(rssServiceModel, newItem);
                }
            }
        }
    }
}