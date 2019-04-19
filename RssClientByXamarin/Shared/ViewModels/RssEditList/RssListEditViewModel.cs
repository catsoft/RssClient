#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using DynamicData;
using JetBrains.Annotations;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Shared.Extensions;
using Shared.Infrastructure.Navigation;
using Shared.Infrastructure.ViewModels;
using Shared.Services.Rss;
using Shared.ViewModels.RssCreate;

#endregion

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

            SourceList = new SourceList<RssServiceModel>();
            SourceList.CountChanged.NotNull().Select(w => w == 0).ToPropertyEx(this, model => model.IsEmpty);

            OpenCreateItemScreenCommand = ReactiveCommand.Create(DoOpenCreateItemScreen).NotNull();
            LoadCommand = ReactiveCommand.CreateFromTask(async token => await _rssService.GetListAsync(token)).NotNull();
            LoadCommand.Subscribe(w =>
            {
                SourceList.Clear();
                SourceList.AddRange(w);
            });

            DeleteItemCommand = ReactiveCommand.CreateFromTask<RssServiceModel>(async (model, token) => await DoDeleteItem(model, token)).NotNull();
            MoveItemCommand = ReactiveCommand
                .CreateFromTask<MoveEventArgs>(async (model, token) => await DoMoveItem(model.NotNull(), token))
                .NotNull();
        }

        [NotNull] public ReactiveCommand<Unit, Unit> OpenCreateItemScreenCommand { get; }

        [NotNull] public ReactiveCommand<Unit, IEnumerable<RssServiceModel>> LoadCommand { get; }

        [NotNull] public ReactiveCommand<RssServiceModel, Unit> DeleteItemCommand { get; }

        [NotNull] public ReactiveCommand<MoveEventArgs, Unit> MoveItemCommand { get; }

        [NotNull] public SourceList<RssServiceModel> SourceList { get; }

        public extern bool IsEmpty { [ObservableAsProperty] get; }

        [NotNull]
        public IObservable<IChangeSet<RssServiceModel>> ConnectChanges() { return SourceList.Connect().NotNull(); }

        private void DoOpenCreateItemScreen() { _navigator.Go(App.Container.Resolve<IWay<RssCreateViewModel>>().NotNull()); }

        [NotNull]
        private async Task DoDeleteItem([CanBeNull] RssServiceModel model, CancellationToken token)
        {
            SourceList.Remove(model);
            await _rssService.RemoveAsync(model?.Id, token);
        }

        private async Task DoMoveItem([NotNull] MoveEventArgs model, CancellationToken token)
        {
            SourceList.Move(model.FromPosition, model.ToPosition);

            var items = SourceList.Items?.ToList() ?? new List<RssServiceModel>();
            for (var i = 0; i < items.Count; i++)
            {
                var localItem = items[i];
                await _rssService.UpdatePositionAsync(localItem?.Id, i, token);
            }
        }
    }
}
