using Shared.Database.Rss;
using Shared.Infrastructure.ViewModels;

namespace Shared.ViewModels.RssMessage
{
    public class RssMessageParameters : ViewModelParameters
    {
        public RssMessageParameters(RssMessageServiceModel rssMessageModel) { RssMessageModel = rssMessageModel; }

        public RssMessageServiceModel RssMessageModel { get; }
    }
}
