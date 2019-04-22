using JetBrains.Annotations;
using Shared.Infrastructure.ViewModels;
using Shared.Services.Rss;

namespace Shared.ViewModels.RssItemDetail
{
    public class RssMessagesListParameters : ViewModelParameters
    {
        public RssMessagesListParameters([NotNull] RssServiceModel rssModel) { RssModel = rssModel; }

        [NotNull] public RssServiceModel RssModel { get; }
    }
}
