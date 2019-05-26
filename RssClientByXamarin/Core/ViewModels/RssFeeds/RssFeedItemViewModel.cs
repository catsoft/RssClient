using System;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Core.Extensions;
using Core.Infrastructure.Dialogs;
using Core.Infrastructure.Navigation;
using Core.Resources;
using Core.Services.RssFeeds;
using Core.ViewModels.Messages.RssFeedMessagesList;
using Core.ViewModels.RssFeeds.Edit;
using DynamicData;
using JetBrains.Annotations;
using ReactiveUI;

namespace Core.ViewModels.RssFeeds
{
    public class RssFeedItemViewModel
    {
        [NotNull] private readonly IRssFeedService _rssFeedService;
        [NotNull] private readonly IDialogService _dialogService;
        [NotNull] private readonly INavigator _navigator;
        [CanBeNull] private readonly SourceList<RssFeedServiceModel> _sourceList;

        public RssFeedItemViewModel([NotNull] IRssFeedService rssFeedService,
            [NotNull] IDialogService dialogService,
            [NotNull] INavigator navigator,
            [CanBeNull] SourceList<RssFeedServiceModel> sourceList)
        {
            _rssFeedService = rssFeedService;
            _dialogService = dialogService;
            _navigator = navigator;
            _sourceList = sourceList;

            ShareCommand = ReactiveCommand.CreateFromTask<RssFeedServiceModel>(DoShare).NotNull();
            ShowDeleteDialogCommand = ReactiveCommand.Create<RssFeedServiceModel>(DoShowDeleteItemDialog).NotNull();
            DeleteItemCommand = ReactiveCommand.CreateFromTask<RssFeedServiceModel>(DoItemRemove).NotNull();
            ReadAllMessagesCommand = ReactiveCommand.CreateFromTask<RssFeedServiceModel>(DoReadAllMessages).NotNull();
            OpenEditItemCommand = ReactiveCommand.Create<RssFeedServiceModel>(DoOpenEditItem).NotNull();
            OpenMessagesListCommand = ReactiveCommand.Create<RssFeedServiceModel>(DoOpenMessagesList).NotNull();
        }

        [NotNull] public ReactiveCommand<RssFeedServiceModel, Unit> ShareCommand { get; }

        [NotNull] public ReactiveCommand<RssFeedServiceModel, Unit> ShowDeleteDialogCommand { get; }

        [NotNull] public ReactiveCommand<RssFeedServiceModel, Unit> DeleteItemCommand { get; }

        [NotNull] public ReactiveCommand<RssFeedServiceModel, Unit> ReadAllMessagesCommand { get; }

        [NotNull] public ReactiveCommand<RssFeedServiceModel, Unit> OpenEditItemCommand { get; }

        [NotNull] public ReactiveCommand<RssFeedServiceModel, Unit> OpenMessagesListCommand { get; }

        private async Task DoShare(RssFeedServiceModel model, CancellationToken token) { await _rssFeedService.ShareAsync(model, token); }

        private void DoShowDeleteItemDialog([CanBeNull] RssFeedServiceModel model)
        {
            _dialogService.ShowYesNoDialog(Strings.RssDeleteDialogTitle,
                "",
                Strings.Yes,
                Strings.No,
                () => DeleteItemCommand.ExecuteIfCan(model),
                null);
        }

        private async Task DoItemRemove(RssFeedServiceModel model)
        {
            _sourceList?.Remove(model);
            await _rssFeedService.RemoveAsync(model?.Id ?? Guid.Empty);
        }

        private async Task DoReadAllMessages([NotNull] RssFeedServiceModel model, CancellationToken token)
        {
            await _rssFeedService.ReadAllMessagesAsync(model.Id, token);
            if (_sourceList != null)
            {
                var newModel = await _rssFeedService.GetAsync(model.Id, token);
                _sourceList.Replace(model, newModel);
            }
        }

        private void DoOpenEditItem([NotNull] RssFeedServiceModel model)
        {
            var parameter = new RssEditParameters(model.Id);
            var typedParameter = new TypedParameter(parameter.GetType(), parameter);
            var editWay = App.Container.Resolve<IWayWithParameters<RssFeedEditViewModel, RssEditParameters>>(typedParameter).NotNull();
            _navigator.Go(editWay);
        }

        private void DoOpenMessagesList([NotNull] RssFeedServiceModel model)
        {
            var parameter = new RssFeedMessagesListParameters(model);
            var typedParameter = new TypedParameter(parameter.GetType(), parameter);
            var way = App.Container.Resolve<IWayWithParameters<RssFeedMessagesListViewModel, RssFeedMessagesListParameters>>(typedParameter)
                .NotNull();
            _navigator.Go(way);
        }
    }
}