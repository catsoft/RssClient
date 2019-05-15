using System.Collections.Generic;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using Core.Extensions;
using Core.Infrastructure.Navigation;
using Core.Infrastructure.ViewModels;
using Core.Repositories.Configurations;
using Core.Services.RssMessages;
using Core.ViewModels.Lists;
using Core.ViewModels.Settings;
using JetBrains.Annotations;
using ReactiveUI;

namespace Core.ViewModels.Messages.FavoriteMessages
{
    public class FavoriteMessagesViewModel : ViewModel
    {
        [NotNull] private readonly IRssMessageService _rssMessageService;

        public FavoriteMessagesViewModel([NotNull] IRssMessageService rssMessageService,
            [NotNull] INavigator navigator,
            [NotNull] IConfigurationRepository configurationRepository)
        {
            _rssMessageService = rssMessageService;
            LoadCommand = ReactiveCommand.CreateFromTask(DoLoad).NotNull();
            ListViewModel = new ListViewModel<RssMessageServiceModel>(LoadCommand);
            AppConfigurationViewModel = new AppConfigurationViewModel(configurationRepository);
            
            RssMessageItemViewModel = new MessageItemViewModel(rssMessageService, navigator, ListViewModel.SourceList, AppConfigurationViewModel);
        }

        [NotNull] public ListViewModel<RssMessageServiceModel> ListViewModel { get; }
        
        [NotNull] public MessageItemViewModel RssMessageItemViewModel { get; }
        
        [NotNull] public ReactiveCommand<Unit, IEnumerable<RssMessageServiceModel>> LoadCommand { get; }
        
        [NotNull] public AppConfigurationViewModel AppConfigurationViewModel { get; }

        private async Task<IEnumerable<RssMessageServiceModel>> DoLoad(CancellationToken token)
        {
            return await _rssMessageService.GetAllFavoriteMessages(token);
        }
    }
}
