using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Core.Configuration.Settings;
using Core.Extensions;
using Core.Infrastructure.Navigation;
using Core.Infrastructure.ViewModels;
using Core.Services.RssMessages;
using Core.ViewModels.Messages.Book;
using Core.ViewModels.Messages.Message;
using Core.ViewModels.Settings;
using DynamicData;
using JetBrains.Annotations;
using ReactiveUI;

namespace Core.ViewModels.Messages
{
    public class MessageItemViewModel : ViewModel
    {
        [NotNull] private readonly INavigator _navigator;
        [NotNull] private readonly IRssMessageService _rssMessageService;
        [CanBeNull] private readonly SourceList<RssMessageServiceModel> _sourceList;
        private readonly AppConfigurationViewModel _configurationViewModel;

        public MessageItemViewModel([NotNull] IRssMessageService rssMessageService, [NotNull] INavigator navigator,
            [CanBeNull] SourceList<RssMessageServiceModel> sourceList, AppConfigurationViewModel configurationViewModel
        )
        {
            _rssMessageService = rssMessageService;
            _navigator = navigator;
            _sourceList = sourceList;
            _configurationViewModel = configurationViewModel;

            HandleItemClickCommand = ReactiveCommand.CreateFromTask<RssMessageServiceModel>(DoHandleItemClick).NotNull();
            ChangeReadItemCommand = ReactiveCommand.CreateFromTask<RssMessageServiceModel>(DoChangeReadItem).NotNull();
            ChangeFavoriteCommand = ReactiveCommand.CreateFromTask<RssMessageServiceModel>(DoChangeFavoriteItem).NotNull();
            ShareItemCommand = ReactiveCommand.CreateFromTask<RssMessageServiceModel>(DoShareItem).NotNull();
        }

        [NotNull] public ReactiveCommand<RssMessageServiceModel, Unit> HandleItemClickCommand { get; }
        
        [NotNull] public ReactiveCommand<RssMessageServiceModel, Unit> ChangeReadItemCommand { get; }
        
        [NotNull] public ReactiveCommand<RssMessageServiceModel, Unit> ChangeFavoriteCommand { get; }
        
        [NotNull] public ReactiveCommand<RssMessageServiceModel, Unit> ShareItemCommand { get; }
        
        [NotNull]
        private async Task DoHandleItemClick([NotNull] RssMessageServiceModel model, CancellationToken token)
        {
            var readerType = _configurationViewModel.AppConfiguration.ReaderType;

            if (readerType == ReaderType.Book)
            {
                NavigateToBook();
            }
            else if (readerType == ReaderType.Strip)
            {
                NavigateToFullMessage(model);
            }

            if (!model.IsRead)
            {
                model.IsRead = true;
                _sourceList?.Replace(model, model);
                await _rssMessageService.UpdateAsync(model, token);
            }
        }

        private void NavigateToFullMessage([NotNull] RssMessageServiceModel model)
        {
            var parameter = new MessageParameters(model);
            var typedParameter = new TypedParameter(parameter.GetType(), parameter);
            var way = App.Container.Resolve<IWayWithParameters<MessageViewModel, MessageParameters>>(typedParameter).NotNull();
            _navigator.Go(way);
        }

        private void NavigateToBook()
        {
            var way = App.Container.Resolve<IWay<BookMessagesViewModel>>();
            _navigator.Go(way);
        }

        [NotNull]
        private async Task DoChangeReadItem([NotNull] RssMessageServiceModel model, CancellationToken token)
        {
            model.IsRead = !model.IsRead;
            _sourceList?.Replace(model, model);
            await _rssMessageService.UpdateAsync(model, token);
        }
        
        [NotNull]
        private async Task DoChangeFavoriteItem([NotNull] RssMessageServiceModel model, CancellationToken token)
        {
            model.IsFavorite = !model.IsFavorite;
            _sourceList?.Replace(model, model);
            await _rssMessageService.UpdateAsync(model, token);
        }

        [NotNull]
        private async Task DoShareItem(RssMessageServiceModel model, CancellationToken token)
        {
            await _rssMessageService.ShareAsync(model, token);
        }
    }
}