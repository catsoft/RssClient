using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
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
using ReactiveUI.Fody.Helpers;

namespace Core.ViewModels.Messages.Book
{
    public class BookMessagesViewModel : ViewModel
    {
        [NotNull] private readonly IRssMessageService _rssMessageService;

        public BookMessagesViewModel([NotNull] IRssMessageService rssMessageService,
            [NotNull] INavigator navigator,
            [NotNull] IConfigurationRepository configurationRepository)
        {
            _rssMessageService = rssMessageService;

            LoadCommand = ReactiveCommand.CreateFromTask(DoLoadCommand).NotNull();

            ListViewModel = new ListViewModel<RssMessageServiceModel>(LoadCommand);

            AppConfigurationViewModel = new AppConfigurationViewModel(configurationRepository);
            MessageItemViewModel = new MessageItemViewModel(rssMessageService, navigator, ListViewModel.SourceList, AppConfigurationViewModel);

            this.WhenAnyValue(model => model.CurrentPosition)
                .NotNull()
                .Where(w => !ListViewModel.IsEmpty)
                .Select(w => CurrentItem)
                .InvokeCommand(MessageItemViewModel.ReadItemCommand);
        }

        [NotNull] public ListViewModel<RssMessageServiceModel> ListViewModel { get; }

        [NotNull] public AppConfigurationViewModel AppConfigurationViewModel { get; }

        [NotNull] public MessageItemViewModel MessageItemViewModel { get; }

        [NotNull] public ReactiveCommand<Unit, IEnumerable<RssMessageServiceModel>> LoadCommand { get; }

        [Reactive] public int CurrentPosition { get; set; }

        [NotNull] public RssMessageServiceModel CurrentItem => ListViewModel.SourceList.Items.NotNull().ElementAt(CurrentPosition).NotNull();

        private Task<IEnumerable<RssMessageServiceModel>> DoLoadCommand(CancellationToken token) { return _rssMessageService.GetAllMessages(token); }
    }
}