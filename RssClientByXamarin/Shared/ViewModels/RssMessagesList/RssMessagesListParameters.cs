#region

using Shared.Infrastructure.ViewModels;
using Shared.Services.Rss;

#endregion

namespace Shared.ViewModels.RssItemDetail
{
    public class RssMessagesListParameters : ViewModelParameters
    {
        public RssMessagesListParameters(RssServiceModel rssModel) { RssModel = rssModel; }

        public RssServiceModel RssModel { get; }
    }
}
