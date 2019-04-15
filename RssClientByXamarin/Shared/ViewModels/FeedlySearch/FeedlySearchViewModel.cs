using System.Collections.Generic;
using System.Threading.Tasks;
using Droid.Repository.Configuration;
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
        }

        public AppConfiguration AppConfiguration { get; }

        public Task<IEnumerable<FeedlyRss>> FindByQueryAsync(string text) => _feedlyService.FindByQueryAsync(text);
    }
}