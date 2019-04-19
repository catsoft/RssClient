#region

using Shared.Infrastructure.ViewModels;

#endregion

namespace Shared.ViewModels.RssItemDetail
{
    public class RssMessagesListViewModel : ViewModelWithParameter<RssMessagesListParameters>
    {
        public RssMessagesListViewModel(RssMessagesListParameters parameters) : base(parameters) { }
    }
}
