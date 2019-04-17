using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Shared.Infrastructure.Navigation;
using Shared.Infrastructure.ViewModels;
using Shared.Services.Rss;
using Shared.ViewModels.RssCreate;

namespace Shared.ViewModels.RssListEdit
{
    public class RssListEditViewModel : ViewModel
    {
        private readonly INavigator _navigator;
        private readonly IRssService _rssService;

        public RssListEditViewModel(INavigator navigator, IRssService rssService)
        {
            _navigator = navigator;
            _rssService = rssService;

            SourceList = new SourceList<RssServiceModel>();
            SourceList.CountChanged.Select(w => w == 0).ToPropertyEx(this, model => model.IsEmpty);
            
            OpenCreateItemScreenCommand = ReactiveCommand.Create(DoOpenCreateItemScreen);
            LoadCommand = ReactiveCommand.CreateFromTask(async token => await _rssService.GetListAsync(token));
            LoadCommand.Subscribe(w =>
            {
                SourceList.Clear();
                SourceList.AddRange(w);
            });
            
            DeleteItemCommand = ReactiveCommand.CreateFromTask<RssServiceModel>(async (model, token) => await DoDeleteItem(model, token));
            MoveItemCommand = ReactiveCommand.CreateFromTask<MoveEventArgs<RssServiceModel>>(async (model, token) => await DoMoveItem(model, token));
        }

        public ReactiveCommand<Unit, Unit> OpenCreateItemScreenCommand { get; }
        
        public ReactiveCommand<Unit, IEnumerable<RssServiceModel>> LoadCommand { get; }
        
        public ReactiveCommand<RssServiceModel, Unit> DeleteItemCommand { get; }
        
        public ReactiveCommand<MoveEventArgs<RssServiceModel>, Unit> MoveItemCommand { get; }
        
        public SourceList<RssServiceModel> SourceList { get; }
        
        public IObservable<IChangeSet<RssServiceModel>> ConnectChanges() => SourceList.Connect();
        
        public extern bool IsEmpty { [ObservableAsProperty] get; }
        
        private void DoOpenCreateItemScreen()
        {
            _navigator.Go(App.Container.Resolve<IWay<RssCreateViewModel>>()); 
        }
        
        private async Task DoDeleteItem(RssServiceModel model, CancellationToken token)
        {
            SourceList.Remove(model);
            await _rssService.RemoveAsync(model.Id, token);
        }
        
        private async Task DoMoveItem(MoveEventArgs<RssServiceModel> model, CancellationToken token)
        {
            SourceList.Move(model.FromPosition, model.ToPosition);

            var items = SourceList.Items.ToList();
            for (var i = 0; i < items.Count; i++)
            {
                var localItem = items[i];
                await _rssService.UpdatePositionAsync(localItem.Id, i, token);
            }
        }
    }
}