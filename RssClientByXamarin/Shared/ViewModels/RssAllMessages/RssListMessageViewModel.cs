using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using DynamicData;
using JetBrains.Annotations;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Shared.Database.Rss;
using Shared.Extensions;
using Shared.Infrastructure.Navigation;
using Shared.Infrastructure.ViewModels;
using Shared.ViewModels.RssMessage;

namespace Shared.ViewModels.RssAllMessages
{
    public class RssListMessageViewModel : ViewModel
    {
        [NotNull] private readonly INavigator _navigator;
        [NotNull] private readonly IRssMessageService _rssMessageService;
        [NotNull] private readonly SourceList<RssMessageServiceModel> _sourceList;
        
        public RssListMessageViewModel([NotNull] IRssMessageService rssMessageService, [NotNull] INavigator navigator, [NotNull] SourceList<RssMessageServiceModel> sourceList)
        {
            _rssMessageService = rssMessageService;
            _navigator = navigator;
            _sourceList = sourceList;
            
            OpenContentScreenCommand = ReactiveCommand.CreateFromTask<RssMessageServiceModel>(DoOpenContentScreen).NotNull();
            ChangeReadItemCommand = ReactiveCommand.CreateFromTask<RssMessageServiceModel>(DoChangeReadItem).NotNull();
            ChangeFavoriteCommand = ReactiveCommand.CreateFromTask<RssMessageServiceModel>(DoChangeFavoriteItem).NotNull();
            ShareItemCommand = ReactiveCommand.CreateFromTask<RssMessageServiceModel>(DoShareItem).NotNull();
        }

        [NotNull] public ReactiveCommand<RssMessageServiceModel, Unit> OpenContentScreenCommand { get; }
        
        [NotNull] public ReactiveCommand<RssMessageServiceModel, Unit> ChangeReadItemCommand { get; }
        
        [NotNull] public ReactiveCommand<RssMessageServiceModel, Unit> ChangeFavoriteCommand { get; }
        
        [NotNull] public ReactiveCommand<RssMessageServiceModel, Unit> ShareItemCommand { get; }
        
        public extern bool IsEmpty { [ObservableAsProperty] get; }
        
        [NotNull]
        private async Task DoOpenContentScreen([NotNull] RssMessageServiceModel model, CancellationToken token)
        {
            var parameter = new RssMessageParameters(model);
            var typedParameter = new TypedParameter(parameter.GetType(), parameter);
            var way = App.Container.Resolve<IWayWithParameters<RssMessage.RssMessageViewModel, RssMessageParameters>>(typedParameter).NotNull();
            _navigator.Go(way);

            if (!model.IsRead)
            {
                model.IsRead = true;
                _sourceList.Replace(model, model);
                await _rssMessageService.UpdateAsync(model, token);
            }
        }

        [NotNull]
        private async Task DoChangeReadItem([NotNull] RssMessageServiceModel model, CancellationToken token)
        {
            model.IsRead = !model.IsRead;
            _sourceList.Replace(model, model);
            await _rssMessageService.UpdateAsync(model, token);
        }
        
        [NotNull]
        private async Task DoChangeFavoriteItem([NotNull] RssMessageServiceModel model, CancellationToken token)
        {
            model.IsFavorite = !model.IsFavorite;
            _sourceList.Replace(model, model);
            await _rssMessageService.UpdateAsync(model, token);
        }

        [NotNull]
        private async Task DoShareItem(RssMessageServiceModel model, CancellationToken token)
        {
            await _rssMessageService.ShareAsync(model, token);
        }
    }
}