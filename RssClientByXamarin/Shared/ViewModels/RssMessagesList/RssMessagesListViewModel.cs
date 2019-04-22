using Shared.Infrastructure.ViewModels;

namespace Shared.ViewModels.RssItemDetail
{
    public class RssMessagesListViewModel : ViewModelWithParameter<RssMessagesListParameters>
    {
        public RssMessagesListViewModel(RssMessagesListParameters parameters) : base(parameters) { }
    }
}
