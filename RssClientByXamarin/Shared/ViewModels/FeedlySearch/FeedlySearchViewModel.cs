using Shared.Infrastructure.ViewModels;
using Shared.Services;

namespace Shared.ViewModels.FeedlySearch
{
    public class FeedlySearchViewModel : ViewModel
    {
        private readonly IFeedlyService _feedlyService;

        public FeedlySearchViewModel(IFeedlyService feedlyService)
        {
            _feedlyService = feedlyService;
        }
    }
}