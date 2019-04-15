using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using Droid.Repository.Configuration;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Shared.Configuration.Settings;
using Shared.Infrastructure.ViewModels;
using Shared.Repository.Feedly;
using Shared.Services;

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

            FindByQueryCommand = ReactiveCommand.CreateFromTask<string, IEnumerable<FeedlyRss>>((query, token) =>
                    _feedlyService.FindByQueryAsync(query, token));

            FindByQueryCommand.ToPropertyEx(this, model => model.FeedlyRss);
        }

        public AppConfiguration AppConfiguration { get; }

        public ReactiveCommand<string, IEnumerable<FeedlyRss>> FindByQueryCommand { get; }
        
        public extern IEnumerable<FeedlyRss> FeedlyRss { [ObservableAsProperty] get; }
    }
}