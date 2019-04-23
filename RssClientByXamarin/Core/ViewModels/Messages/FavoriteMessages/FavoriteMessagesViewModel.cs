using System.Collections.Generic;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using Core.Configuration.Settings;
using Core.Extensions;
using Core.Infrastructure.Navigation;
using Core.Infrastructure.ViewModels;
using Core.Repositories.Configuration;
using Core.Services.RssMessages;
using Core.ViewModels.Lists;
using JetBrains.Annotations;
using ReactiveUI;

namespace Core.ViewModels.Messages.FavoriteMessages
{
    public class FavoriteMessagesViewModel : ViewModel
    {
        [NotNull] private readonly IRssMessageService _rssMessageService;
        [NotNull] private readonly IConfigurationRepository _configurationRepository;

        public FavoriteMessagesViewModel([NotNull] IRssMessageService rssMessageService,
            [NotNull] INavigator navigator,
            [NotNull] IConfigurationRepository configurationRepository)
        {
            _rssMessageService = rssMessageService;
            _configurationRepository = configurationRepository;
            LoadCommand = ReactiveCommand.CreateFromTask(DoLoad).NotNull();
            ListViewModel = new ListViewModel<RssMessageServiceModel>(LoadCommand);
            RssMessageItemViewModel = new MessageItemViewModel(rssMessageService, navigator, ListViewModel.SourceList);
        }

        [NotNull] public ListViewModel<RssMessageServiceModel> ListViewModel { get; }
        
        [NotNull] public MessageItemViewModel RssMessageItemViewModel { get; }
        
        [NotNull] public ReactiveCommand<Unit, IEnumerable<RssMessageServiceModel>> LoadCommand { get; }
        
        [NotNull] public AppConfiguration AppConfiguration => _configurationRepository.GetSettings<AppConfiguration>();

        private async Task<IEnumerable<RssMessageServiceModel>> DoLoad(CancellationToken token)
        {
            return await _rssMessageService.GetAllFavoriteMessages(token);
        }
    }
}
