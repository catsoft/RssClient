using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using Core.Infrastructure.Navigation;
using Core.Infrastructure.ViewModels;
using Core.Repositories.Configurations;
using Core.Services.RssMessages;
using Core.ViewModels.Lists;
using Core.ViewModels.Settings;
using JetBrains.Annotations;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Core.ViewModels.Messages.Book
{
    public class BookMessagesViewModel : ViewModel
    {
        private readonly IRssMessageService _rssMessageService;

        public BookMessagesViewModel(IRssMessageService rssMessageService, INavigator navigator, IConfigurationRepository configurationRepository)
        {
            _rssMessageService = rssMessageService;
            
            LoadCommand = ReactiveCommand.CreateFromTask(DoLoadCommand);
                
            ListViewModel = new ListViewModel<RssMessageServiceModel>(LoadCommand);
            
            AppConfigurationViewModel = new AppConfigurationViewModel(configurationRepository);
            MessageItemViewModel = new MessageItemViewModel(rssMessageService, navigator, ListViewModel.SourceList, AppConfigurationViewModel);
        }
        
        [NotNull] public ListViewModel<RssMessageServiceModel> ListViewModel { get; }
        
        [NotNull] public AppConfigurationViewModel AppConfigurationViewModel { get; }
        
        [NotNull] public MessageItemViewModel MessageItemViewModel{ get; }
        
        [NotNull] public ReactiveCommand<Unit, IEnumerable<RssMessageServiceModel>> LoadCommand { get; }

        [Reactive]
        public int CurrentPosition { get; set; }

        [NotNull]
        public RssMessageServiceModel CurrentItem => ListViewModel.SourceList.Items.ElementAt(CurrentPosition);
        
        private Task<IEnumerable<RssMessageServiceModel>> DoLoadCommand(CancellationToken token)
        {
            return _rssMessageService.GetAllMessages(token);
        }
    }
}