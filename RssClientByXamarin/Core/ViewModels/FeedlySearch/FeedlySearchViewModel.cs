using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Configuration.Settings;
using Core.Extensions;
using Core.Infrastructure.Dialogs;
using Core.Infrastructure.ViewModels;
using Core.Repositories.Configurations;
using Core.Repositories.Feedly;
using Core.Resources;
using Core.Services.Feedly;
using Core.ViewModels.Lists;
using Core.ViewModels.RssFeeds.RssFeedsUpdater;
using JetBrains.Annotations;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Core.ViewModels.FeedlySearch
{
    public class FeedlySearchViewModel : ViewModel
    {
        [NotNull] private readonly IFeedlySearchService _feedlySearchService;
        [NotNull] private readonly IConfigurationRepository _configurationRepository;
        [NotNull] private readonly IToastService _toastService;
        [NotNull] private readonly RssFeedsUpdaterViewModel _rssFeedsUpdaterViewModel;

        public FeedlySearchViewModel([NotNull] IFeedlySearchService feedlySearchService, 
            [NotNull] IConfigurationRepository configurationRepository,
            [NotNull] IToastService toastService,
            [NotNull] RssFeedsUpdaterViewModel rssFeedsUpdaterViewModel)
        {
            _feedlySearchService = feedlySearchService;
            _configurationRepository = configurationRepository;
            _toastService = toastService;
            _rssFeedsUpdaterViewModel = rssFeedsUpdaterViewModel;

            FindByQueryCommand = ReactiveCommand.CreateFromTask(token => _feedlySearchService.FindByQueryAsync(SearchQuery ?? "", token),
                    this.WhenAnyValue(model => model.SearchQuery).NotNull().Select(w => !string.IsNullOrEmpty(w)))
                .NotNull();

            ListViewModel = new ListViewModel<FeedlyRssDomainModel>(FindByQueryCommand);

            FindByQueryCommand.Select(w => w == null || !w.Any()).ToPropertyEx(this, model => model.IsEmpty, true);

            AddFeedlyRssCommand = ReactiveCommand.CreateFromTask<FeedlyRssDomainModel>(DoAddFeedlyRss).NotNull();

            this.WhenAnyValue(vm => vm.SearchQuery)
                .NotNull()
                .Throttle(TimeSpan.FromSeconds(0.35f))
                .SelectUnit()
                .InvokeCommand(FindByQueryCommand);
        }

        [NotNull] public ListViewModel<FeedlyRssDomainModel> ListViewModel { get; }

        [NotNull] public AppConfiguration AppConfiguration => _configurationRepository.GetSettings<AppConfiguration>();

        [Reactive] [CanBeNull] public string SearchQuery { get; set; }

        [NotNull] public ReactiveCommand<Unit, IEnumerable<FeedlyRssDomainModel>> FindByQueryCommand { get; }

        [NotNull] public ReactiveCommand<FeedlyRssDomainModel, Unit> AddFeedlyRssCommand { get; }

        public extern bool IsEmpty { [ObservableAsProperty] get; }

        [NotNull]
        private async Task DoAddFeedlyRss([NotNull] FeedlyRssDomainModel model, CancellationToken token = default)
        {
            _toastService.Show(Strings.RssAdded + model.Title);
            
            await _feedlySearchService.AddFeedly(model, token);

            _rssFeedsUpdaterViewModel.SoftUpdateCommand.ExecuteIfCan();
        }
    }
}
