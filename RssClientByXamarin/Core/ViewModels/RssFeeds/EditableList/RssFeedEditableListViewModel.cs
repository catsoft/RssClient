using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Core.Extensions;
using Core.Infrastructure.Navigation;
using Core.Infrastructure.ViewModels;
using Core.Services.RssFeeds;
using Core.ViewModels.Lists;
using Core.ViewModels.RssFeeds.Create;
using DynamicData;
using JetBrains.Annotations;
using ReactiveUI;

namespace Core.ViewModels.RssFeeds.EditableList
{
    public class RssFeedEditableListViewModel : ViewModel
    {
        [NotNull] private readonly INavigator _navigator;
        [NotNull] private readonly IRssFeedService _rssFeedService;

        public RssFeedEditableListViewModel([NotNull] INavigator navigator, [NotNull] IRssFeedService rssFeedService)
        {
            _navigator = navigator;
            _rssFeedService = rssFeedService;

            LoadCommand = ReactiveCommand.CreateFromTask(async token => await _rssFeedService.GetListAsync(token)).NotNull();
            ListViewModel = new ListViewModel<RssFeedServiceModel>(LoadCommand);
            
            OpenCreateItemScreenCommand = ReactiveCommand.Create(DoOpenCreateItemScreen).NotNull();

            DeleteItemCommand = ReactiveCommand.CreateFromTask<RssFeedServiceModel>(async (model, token) => await DoDeleteItem(model, token)).NotNull();
            MoveItemCommand = ReactiveCommand
                .CreateFromTask<MoveEventArgs>(async (model, token) => await DoMoveItem(model.NotNull(), token))
                .NotNull();
        }

        [NotNull] public ListViewModel<RssFeedServiceModel> ListViewModel { get; }

        [NotNull] public ReactiveCommand<Unit, Unit> OpenCreateItemScreenCommand { get; }

        [NotNull] public ReactiveCommand<Unit, IEnumerable<RssFeedServiceModel>> LoadCommand { get; }

        [NotNull] public ReactiveCommand<RssFeedServiceModel, Unit> DeleteItemCommand { get; }

        [NotNull] public ReactiveCommand<MoveEventArgs, Unit> MoveItemCommand { get; }

        private void DoOpenCreateItemScreen() { _navigator.Go(App.Container.Resolve<IWay<RssFeedCreateViewModel>>().NotNull()); }

        [NotNull]
        private async Task DoDeleteItem([CanBeNull] RssFeedServiceModel model, CancellationToken token)
        {
            ListViewModel.SourceList.Remove(model);
            await _rssFeedService.RemoveAsync(model?.Id, token);
        }

        private async Task DoMoveItem([NotNull] MoveEventArgs model, CancellationToken token)
        {
            ListViewModel.SourceList.Move(model.FromPosition, model.ToPosition);

            var items = ListViewModel.SourceList.Items?.ToList() ?? new List<RssFeedServiceModel>();
            for (var i = 0; i < items.Count; i++)
            {
                var localItem = items[i];
                await _rssFeedService.UpdatePositionAsync(localItem?.Id, i, token);
            }
        }
    }
}
