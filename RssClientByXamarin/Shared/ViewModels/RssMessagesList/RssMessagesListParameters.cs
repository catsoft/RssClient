using Shared.Infrastructure.ViewModels;
using Shared.Services.Rss;

namespace Shared.ViewModels.RssItemDetail
{
    public class RssMessagesListParameters : ViewModelParameters
    {
        public RssMessagesListParameters(RssServiceModel rssModel) { RssModel = rssModel; }

        public RssServiceModel RssModel { get; }
    }
}
