using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Droid.Repositories.Configuration;
using DynamicData;
using JetBrains.Annotations;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Shared.Configuration.Settings;
using Shared.Extensions;
using Shared.Infrastructure.ViewModels;
using Shared.Repositories.Feedly;
using Shared.Services.Feedly;
using Shared.ViewModels.RssAllMessages;

namespace Shared.ViewModels.FeedlySearch
{
    public class FeedlySearchViewModel : ViewModel
    {
        [NotNull] private readonly IFeedlyService _feedlyService;
        [NotNull] private readonly IConfigurationRepository _configurationRepository;

        public FeedlySearchViewModel([NotNull] IFeedlyService feedlyService, [NotNull] IConfigurationRepository configurationRepository)
        {
            _feedlyService = feedlyService;
            _configurationRepository = configurationRepository;

            FindByQueryCommand = ReactiveCommand.CreateFromTask(token => _feedlyService.FindByQueryAsync(SearchQuery ?? "", token),
                    this.WhenAnyValue(model => model.SearchQuery).NotNull().Select(w => !string.IsNullOrEmpty(w)))
                .NotNull();
            
            ListViewModel = new ListViewModel<FeedlyRssDomainModel>(FindByQueryCommand);
         
            FindByQueryCommand.Select(w => w == null || !w.Any()).ToPropertyEx(this, model => model.IsEmpty, true);

            AddFeedlyRssCommand = ReactiveCommand.CreateFromTask<FeedlyRssDomainModel>(DoAddFeedlyRss).NotNull();

            this.WhenAnyValue(vm => vm.SearchQuery)
                .NotNull()
                .Throttle(TimeSpan.FromSeconds(0.35f))
                .InvokeCommand(FindByQueryCommand);
        }

        [NotNull] public ListViewModel<FeedlyRssDomainModel> ListViewModel { get; }

        [NotNull] public AppConfiguration AppConfiguration => _configurationRepository.GetSettings<AppConfiguration>();

        [Reactive] [CanBeNull] public string SearchQuery { get; set; }

        [NotNull] public ReactiveCommand<Unit, IEnumerable<FeedlyRssDomainModel>> FindByQueryCommand { get; }

        [NotNull] public ReactiveCommand<FeedlyRssDomainModel, Unit> AddFeedlyRssCommand { get; }

        public extern bool IsEmpty { [ObservableAsProperty] get; }

        [NotNull]
        private Task DoAddFeedlyRss([NotNull] FeedlyRssDomainModel model, CancellationToken token = default)
        {
            return _feedlyService.AddFeedly(model, token);

            //TODO го тоаст
//                Activity.Toast(Activity.GetText(Resource.String.recommended_rss_add_rss_toast) + viewHolder.Item.Title);
        }
    }
}
