using System;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Droid.EmbeddedResourse;
using DynamicData;
using JetBrains.Annotations;
using ReactiveUI;
using Shared.Extensions;
using Shared.Infrastructure.Dialogs;
using Shared.Infrastructure.Navigation;
using Shared.Services.Rss;
using Shared.ViewModels.RssEdit;
using Shared.ViewModels.RssItemDetail;

namespace Shared.ViewModels
{
    public class RssViewModel
    {
        [NotNull] private readonly IRssService _rssService;
        [NotNull] private readonly IDialogService _dialogService;
        [NotNull] private readonly INavigator _navigator;
        [CanBeNull] private readonly SourceList<RssServiceModel> _sourceList;

        public RssViewModel(IRssService rssService, IDialogService dialogService, [NotNull] INavigator navigator, [CanBeNull] SourceList<RssServiceModel> sourceList)
        {
            _rssService = rssService;
            _dialogService = dialogService;
            _navigator = navigator;
            _sourceList = sourceList;

            ShareCommand = ReactiveCommand.CreateFromTask<RssServiceModel>(DoShare);
            ShowDeleteDialogCommand = ReactiveCommand.Create<RssServiceModel>(DoShowDeleteItemDialog);
            DeleteItemCommand = ReactiveCommand.CreateFromTask<RssServiceModel>(DoItemRemove);
            ReadAllMessagesCommand = ReactiveCommand.CreateFromTask<RssServiceModel>(DoReadAllMessages);
            OpenEditItemCommand = ReactiveCommand.Create<RssServiceModel>(DoOpenEditItem);
            OpenMessagesListCommand = ReactiveCommand.Create<RssServiceModel>(DoOpenMessagesList);
        }

        [NotNull] public ReactiveCommand<RssServiceModel, Unit> ShareCommand { get; }
        
        [NotNull] public ReactiveCommand<RssServiceModel, Unit> ShowDeleteDialogCommand { get; }
        
        [NotNull] public ReactiveCommand<RssServiceModel, Unit> DeleteItemCommand { get; }
        
        [NotNull] public ReactiveCommand<RssServiceModel, Unit> ReadAllMessagesCommand { get; }
        
        [NotNull] public ReactiveCommand<RssServiceModel, Unit> OpenEditItemCommand { get; }
        
        [NotNull] public ReactiveCommand<RssServiceModel, Unit> OpenMessagesListCommand { get; }
        
        private async Task DoShare(RssServiceModel model, CancellationToken token)
        {
            await _rssService.ShareAsync(model, token);
        }

        private void DoShowDeleteItemDialog([CanBeNull] RssServiceModel model)
        {
            _dialogService.ShowYesNoDialog(Strings.RssDeleteDialogTitle,
                "",
                Strings.Yes,
                Strings.No,
                () => DeleteItemCommand.Execute().Subscribe(),
                null);
        }

        private async Task DoItemRemove(RssServiceModel model)
        {
            _sourceList?.Remove(model);
            await _rssService.RemoveAsync(model?.Id);
        }
        
        private async Task DoReadAllMessages(RssServiceModel model, CancellationToken token)
        {
            await _rssService.ReadAllMessagesAsync(model.Id, token);
            if (_sourceList != null)
            {
                var newModel = await _rssService.GetAsync(model.Id, token);
                _sourceList.Replace(model, newModel);
            }
        }

        private void DoOpenEditItem(RssServiceModel model)
        {
            var parameter = new RssEditParameters(model.Id);
            var typedParameter = new TypedParameter(parameter.GetType(), parameter);
            var editWay = App.Container.Resolve<IWayWithParameters<RssEditViewModel, RssEditParameters>>(typedParameter);
            _navigator.Go(editWay);
        }
        
        private void DoOpenMessagesList(RssServiceModel model)
        {
            var parameter = new RssMessagesListParameters(model);
            var typedParameter = new TypedParameter(parameter.GetType(), parameter);
            var way = App.Container.Resolve<IWayWithParameters<RssMessagesListViewModel, RssMessagesListParameters>>(typedParameter).NotNull();
            _navigator.Go(way);
        }
    }
}