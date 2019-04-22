using System.Collections.Generic;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using Droid.Repositories.Configuration;
using JetBrains.Annotations;
using ReactiveUI;
using Shared.Configuration.Settings;
using Shared.Database.Rss;
using Shared.Extensions;
using Shared.Infrastructure.Navigation;
using Shared.Infrastructure.ViewModels;
using Shared.ViewModels.RssAllMessages;

namespace Shared.ViewModels.RssFavoriteMessages
{
    public class RssFavoriteMessagesViewModel : ViewModel
    {
        [NotNull] private readonly IRssMessageService _rssMessageService;
        [NotNull] private readonly IConfigurationRepository _configurationRepository;

        public RssFavoriteMessagesViewModel([NotNull] IRssMessageService rssMessageService,
            [NotNull] INavigator navigator,
            [NotNull] IConfigurationRepository configurationRepository)
        {
            _rssMessageService = rssMessageService;
            _configurationRepository = configurationRepository;
            LoadCommand = ReactiveCommand.CreateFromTask(DoLoad).NotNull();
            ListViewModel = new ListViewModel<RssMessageServiceModel>(LoadCommand);
            RssMessageViewModel = new RssListMessageViewModel(rssMessageService, navigator, ListViewModel.SourceList);
        }

        [NotNull] public ListViewModel<RssMessageServiceModel> ListViewModel { get; }
        
        [NotNull] public RssListMessageViewModel RssMessageViewModel { get; }
        
        [NotNull] public ReactiveCommand<Unit, IEnumerable<RssMessageServiceModel>> LoadCommand { get; }
        
        [NotNull] public AppConfiguration AppConfiguration => _configurationRepository.GetSettings<AppConfiguration>();

        private async Task<IEnumerable<RssMessageServiceModel>> DoLoad(CancellationToken token)
        {
            return await _rssMessageService.GetAllFavoriteMessages(token);
        }
    }
}
