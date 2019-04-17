using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Droid.Repository.Configuration;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Shared.Configuration.Settings;
using Shared.Infrastructure.ViewModels;
using Shared.Repository.Feedly;
using Shared.Services;
using Shared.Services.Feedly;
using Shared.ViewModels.Settings;

namespace Shared.ViewModels.FeedlySearch
{
    public class FeedlySearchViewModel : ViewModel
    {
        private readonly IFeedlyService _feedlyService;
        private readonly IConfigurationRepository _configurationRepository;

        public FeedlySearchViewModel(IFeedlyService feedlyService, IConfigurationRepository configurationRepository)
        {
            _feedlyService = feedlyService;
            _configurationRepository = configurationRepository;

            AppConfiguration = _configurationRepository.GetSettings<AppConfiguration>();

            FindByQueryCommand = ReactiveCommand.CreateFromTask<string, IEnumerable<FeedlyRss>>(
                (query, token) => _feedlyService.FindByQueryAsync(query ?? "", token),
                this.WhenAnyValue(model => model.SearchQuery).Select(w => !string.IsNullOrEmpty(w)));
            
            FindByQueryCommand.ToPropertyEx(this, model => model.FeedlyRss);
            FindByQueryCommand.Select(w => w == null || !w.Any()).ToPropertyEx(this, model => model.IsEmpty, true);
            
            this.WhenAnyValue(vm => vm.SearchQuery)
                .Throttle(TimeSpan.FromSeconds(0.35))
                .InvokeCommand(FindByQueryCommand);
        }

        public AppConfiguration AppConfiguration { get; }

        [Reactive]
        public string SearchQuery { get; set; }
        
        public ReactiveCommand<string, IEnumerable<FeedlyRss>> FindByQueryCommand { get; }
        
        public extern IEnumerable<FeedlyRss> FeedlyRss { [ObservableAsProperty] get; }

        public extern bool IsEmpty { [ObservableAsProperty] get; }
    }
}