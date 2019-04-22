using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using DynamicData;
using JetBrains.Annotations;
using ReactiveUI;
using Shared.Extensions;
using Shared.Infrastructure.Navigation;
using Shared.Infrastructure.ViewModels;
using Shared.Services.Rss;
using Shared.ViewModels.RssAllMessages;
using Shared.ViewModels.RssCreate;

namespace Shared.ViewModels.RssListEdit
{
    public class RssListEditViewModel : ViewModel
    {
        [NotNull] private readonly INavigator _navigator;
        [NotNull] private readonly IRssService _rssService;

        public RssListEditViewModel([NotNull] INavigator navigator, [NotNull] IRssService rssService)
        {
            _navigator = navigator;
            _rssService = rssService;

            LoadCommand = ReactiveCommand.CreateFromTask(async token => await _rssService.GetListAsync(token)).NotNull();
            ListViewModel = new ListViewModel<RssServiceModel>(LoadCommand);
            
            OpenCreateItemScreenCommand = ReactiveCommand.Create(DoOpenCreateItemScreen).NotNull();

            DeleteItemCommand = ReactiveCommand.CreateFromTask<RssServiceModel>(async (model, token) => await DoDeleteItem(model, token)).NotNull();
            MoveItemCommand = ReactiveCommand
                .CreateFromTask<MoveEventArgs>(async (model, token) => await DoMoveItem(model.NotNull(), token))
                .NotNull();
        }

        [NotNull] public ListViewModel<RssServiceModel> ListViewModel { get; }

        [NotNull] public ReactiveCommand<Unit, Unit> OpenCreateItemScreenCommand { get; }

        [NotNull] public ReactiveCommand<Unit, IEnumerable<RssServiceModel>> LoadCommand { get; }

        [NotNull] public ReactiveCommand<RssServiceModel, Unit> DeleteItemCommand { get; }

        [NotNull] public ReactiveCommand<MoveEventArgs, Unit> MoveItemCommand { get; }

        private void DoOpenCreateItemScreen() { _navigator.Go(App.Container.Resolve<IWay<RssCreateViewModel>>().NotNull()); }

        [NotNull]
        private async Task DoDeleteItem([CanBeNull] RssServiceModel model, CancellationToken token)
        {
            ListViewModel.SourceList.Remove(model);
            await _rssService.RemoveAsync(model?.Id, token);
        }

        private async Task DoMoveItem([NotNull] MoveEventArgs model, CancellationToken token)
        {
            ListViewModel.SourceList.Move(model.FromPosition, model.ToPosition);

            var items = ListViewModel.SourceList.Items?.ToList() ?? new List<RssServiceModel>();
            for (var i = 0; i < items.Count; i++)
            {
                var localItem = items[i];
                await _rssService.UpdatePositionAsync(localItem?.Id, i, token);
            }
        }
    }
}
