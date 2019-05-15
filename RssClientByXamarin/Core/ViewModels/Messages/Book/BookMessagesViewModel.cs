using System.Collections.Generic;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using Core.Infrastructure.ViewModels;
using Core.Services.RssMessages;
using Core.ViewModels.Lists;
using JetBrains.Annotations;
using ReactiveUI;

namespace Core.ViewModels.Messages.Book
{
    public class BookMessagesViewModel : ViewModel
    {
        private readonly IRssMessageService _rssMessageService;

        public BookMessagesViewModel(IRssMessageService rssMessageService)
        {
            _rssMessageService = rssMessageService;
            
            LoadCommand = ReactiveCommand.CreateFromTask(DoLoadCommand);
                
            ListViewModel = new ListViewModel<RssMessageServiceModel>(LoadCommand);
        }
        
        [NotNull] public ListViewModel<RssMessageServiceModel> ListViewModel { get; }
        
        [NotNull] public ReactiveCommand<Unit, IEnumerable<RssMessageServiceModel>> LoadCommand { get; }

        private Task<IEnumerable<RssMessageServiceModel>> DoLoadCommand(CancellationToken token)
        {
            return _rssMessageService.GetAllMessages(token);
        }
    }
}